using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using Machine.X64.Runtime.Core;
using Machine.X64.Runtime.Errors;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

/// <summary>
/// The emulation of the Central Processing Unit (CPU), mimicking features from both Intel 64 and
/// AMD 64 processors.
/// </summary>
public sealed partial class CpuRuntime(int memorySize = 65536, int ioPortCount = 256)
{
    /// <summary>
    /// The floating-point unit.
    /// </summary>
    public Fpu Fpu { get; set; } = new Fpu();

    /// <summary>
    /// The processor memory.
    /// </summary>
    public Memory Memory { get; set; } = new Memory(memorySize);

    /// <summary>
    /// All registers of this processor.
    /// </summary>
    public ProcessorRegisters ProcessorRegisters { get; set; } = new ProcessorRegisters();

    /// <summary>
    /// Address of the IDT.
    /// </summary>
    public uint IDTAddress { get; set; }

    /// <summary>
    /// IDT size.
    /// </summary>
    public uint IDTLimit { get; set; }

    /// <summary>
    /// Specifies whether the CPU is actively running.
    /// </summary>
    public bool Busy { get; set; }

    /// <summary>
    /// RIP to the beginning of the current instruction.
    /// </summary>
    public ulong CurrentRip { get; set; }

    /// <summary>
    /// Is MMX mode enabled? (Default value is false).
    /// </summary>
    public bool MmxMode { get; set; } = false;

    /// <summary>
    /// Last (or executing) instruction.
    /// </summary>
    public Instruction LastOrExecutingInstruction { get; set; }

    /// <summary>
    /// The MXCSR register.
    /// </summary>
    public Mxcsr Mxcsr { get; set; }

    /// <summary>
    /// I/O ports.
    /// </summary>
    public InputOutputPort[] IOPorts { get; set; } = new InputOutputPort[ioPortCount];

    /// <summary>
    /// Time Stamp Counter register.
    /// </summary>
    public ulong Tsc { get; set; }

    /// <summary>
    /// The CPL (Current Privilege Level) in CS.
    /// </summary>
    public byte Cpl => this.ProcessorRegisters.GetCpl();

    private ulong ControlMaskM
    {
        get => ProcessorRegisters.K0;
        set => ProcessorRegisters.K0 = value;
    }

    /// <summary>
    /// Checks if the CPU is running in protected mode.
    /// </summary>
    public bool IsRunningInProtectedMode
    {
        get => ProcessorRegisters.CR0PE;
        set => ProcessorRegisters.CR0PE = value;
    }

    private ReadOnlySpan<bool> GetControlMaskMData4()
    {
        return BitManager.GetBitsBigEndian(ControlMaskM);
    }

    private void ProcessFlags(Int128 result)
    {
        bool msbIsNotZero = IsMsbNotZero(result);
        if (msbIsNotZero)
        {
            ProcessorRegisters.RFlagsOF = result > ulong.MaxValue;
            ProcessorRegisters.RFlagsSF = false;
        }
        else
        {
            ProcessorRegisters.RFlagsSF = true;
        }

        ProcessorRegisters.RFlagsPF = result % 2 == 0;
        ProcessorRegisters.RFlagsZF = result == 0;
    }

    private void ProcessFlags(ushort result)
    {
        bool msbIsNotZero = IsMsbNotZero(result);
        if (msbIsNotZero)
        {
            ProcessorRegisters.RFlagsOF = result > byte.MaxValue;
            ProcessorRegisters.RFlagsSF = false;
        }
        else
        {
            ProcessorRegisters.RFlagsSF = true;
        }

        ProcessorRegisters.RFlagsPF = result % 2 == 0;
        ProcessorRegisters.RFlagsZF = result == 0;
    }

    private void ProcessFlags(uint result)
    {
        bool msbIsNotZero = IsMsbNotZero(result);
        if (msbIsNotZero)
        {
            ProcessorRegisters.RFlagsOF = result > ushort.MaxValue;
            ProcessorRegisters.RFlagsSF = false;
        }
        else
        {
            ProcessorRegisters.RFlagsSF = true;
        }

        ProcessorRegisters.RFlagsPF = result % 2 == 0;
        ProcessorRegisters.RFlagsZF = result == 0;
    }

    private void ProcessFlags(ulong result)
    {
        bool msbIsNotZero = IsMsbNotZero(result);
        if (msbIsNotZero)
        {
            ProcessorRegisters.RFlagsOF = result > uint.MaxValue;
            ProcessorRegisters.RFlagsSF = false;
        }
        else
        {
            ProcessorRegisters.RFlagsSF = true;
        }

        ProcessorRegisters.RFlagsPF = result % 2 == 0;
        ProcessorRegisters.RFlagsZF = result == 0;
    }

    private static bool IsMsbNotZero(Int128 value)
    {
        return (value & (0x8000000000000000 << 32)) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsMsbNotZero(ushort value)
    {
        return (value & 0x8000) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsMsbNotZero(uint value)
    {
        return (value & 0x80000000) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsMsbNotZero(ulong value)
    {
        return (value & 0x8000000000000000) != 0;
    }

    private static bool IsRMMemory(in Instruction instruction)
    {
        return instruction.GetOpRegister(0) == Register.None;
    }

    private ulong GetMemOperand8(Instruction instruction)
    {
        if (instruction.MemoryBase != Register.None && instruction.MemoryIndex != Register.None)
        {
            Register baseReg = instruction.MemoryBase;
            Register segmentReg = instruction.MemorySegment;
            int addr = (ProcessorRegisters.EvaluateRegisterValue8(baseReg) * 16) + ProcessorRegisters.EvaluateRegisterValue8(segmentReg);
            return (ulong)addr;
        }
        else
        {
            return instruction.MemoryDisplacement32;
        }
    }

    private ulong GetMemOperand16(Instruction instruction)
    {
        if (instruction.MemoryBase != Register.None && instruction.MemoryIndex != Register.None)
        {
            Register baseReg = instruction.MemoryBase;
            Register segmentReg = instruction.MemorySegment;
            int addr = (ProcessorRegisters.EvaluateRegisterValue16(baseReg) * 16) + ProcessorRegisters.EvaluateRegisterValue16(segmentReg);
            return (ulong)addr;
        }
        else
        {
            return instruction.MemoryDisplacement32;
        }
    }

    private ulong GetMemOperand32(Instruction instruction)
    {
        if (instruction.MemoryBase != Register.None && instruction.MemoryIndex != Register.None)
        {
            Register baseReg = instruction.MemoryBase;
            Register segmentReg = instruction.MemorySegment;
            uint addr = (ProcessorRegisters.EvaluateRegisterValue32(baseReg) * 16) + ProcessorRegisters.EvaluateRegisterValue32(segmentReg);
            return addr;
        }
        else
        {
            return instruction.MemoryDisplacement32;
        }
    }

    private ulong GetMemOperand64(Instruction instruction)
    {
        if (instruction.MemoryBase != Register.None && instruction.MemoryIndex != Register.None)
        {
            Register baseReg = instruction.MemoryBase;
            Register segmentReg = instruction.MemorySegment;
            ulong addr = (ProcessorRegisters.EvaluateRegisterValue64(baseReg) * 16) + ProcessorRegisters.EvaluateRegisterValue64(segmentReg);
            return addr;
        }
        else
        {
            return instruction.MemoryDisplacement64;
        }
    }

    private void RMSet8(in Instruction instruction, byte value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand8(instruction), value);
        else
            ProcessorRegisters.WriteToRegister8(instruction.GetOpRegister(registerPositionCase), value);
    }

    private void RMSet16(in Instruction instruction, ushort value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand16(instruction), value);
        else
            ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(registerPositionCase), value);
    }

    private void RMSet32(in Instruction instruction, uint value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand32(instruction), value);
        else
            ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(registerPositionCase), value);
    }

    private void RMSet64(in Instruction instruction, ulong value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand64(instruction), value);
        else
            ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(registerPositionCase), value);
    }

    private byte RMEvaluate8(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory8(GetMemOperand8(instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(registerPositionCase));
    }

    private ushort RMEvaluate16(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory16(GetMemOperand16(instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(registerPositionCase));
    }

    private uint RMEvaluate32(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory32(GetMemOperand32(instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(registerPositionCase));
    }

    private ulong RMEvaluate64(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory64(GetMemOperand64(instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(registerPositionCase));
    }

    private void AlterObjectAtMemory(ulong addr, byte value)
    {
        Memory[addr] = value;
    }

    private void AlterObjectAtMemory(ulong addr, ushort value)
    {
        Memory.WriteUInt16(addr, value);
    }

    private void AlterObjectAtMemory(ulong addr, uint value)
    {
        Memory.WriteUInt32(addr, value);
    }

    private void AlterObjectAtMemory(ulong addr, ulong value)
    {
        Memory.WriteUInt64(addr, value);
    }

    private byte QueryObjectAtMemory8(ulong addr)
    {
        return Memory[addr];
    }

    private ushort QueryObjectAtMemory16(ulong addr)
    {
        return Memory.ReadUInt16(addr);
    }

    private uint QueryObjectAtMemory32(ulong addr)
    {
        return Memory.ReadUInt32(addr);
    }

    private ulong QueryObjectAtMemory64(ulong addr)
    {
        return Memory.ReadUInt64(addr);
    }

    private static void ReportInvalidCodeUnderMnemonic(Code code, Mnemonic mnemonic)
    {
        throw new InvalidOperationException($"Code '{code}' does not match mnemonic '{mnemonic}'");
    }

    private static bool GetParityOfLowerEightBits(ushort value)
    {
        byte num = (byte)(value & 0xff);
        byte total;
        for (total = 0; num > 0; total++)
        {
            num &= (byte)(num - 1);
        }
        return (total % 2) == 0;
    }

    private bool HasBitSetInK1(int bit)
    {
        return BitUtilities.IsBitSet(ProcessorRegisters.K1, bit);
    }

    private Vector128<float> EvaluateXmmFromInstruction(in Instruction instruction, int operand)
    {
        Vector128<float> value2;
        if (instruction.GetOpKind(operand) == OpKind.Memory)
        {
            value2 = this.Memory.ReadBinaryVector128(GetMemOperand64(instruction));
        }
        else
        {
            value2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(operand));
        }
        return value2;
    }

    private Vector256<float> EvaluateYmmFromInstruction(in Instruction instruction, int operand)
    {
        Vector256<float> value2;
        if (instruction.GetOpKind(operand) == OpKind.Memory)
        {
            value2 = this.Memory.ReadBinaryVector256(GetMemOperand64(instruction));
        }
        else
        {
            value2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(operand));
        }
        return value2;
    }

    private Vector512<float> EvaluateZmmFromInstruction(in Instruction instruction, int operand)
    {
        Vector512<float> value2;
        if (instruction.GetOpKind(operand) == OpKind.Memory)
        {
            value2 = this.Memory.ReadBinaryVector512(GetMemOperand64(instruction));
        }
        else
        {
            value2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(operand));
        }
        return value2;
    }

    private ulong GetOpMask(Register register)
    {
        if (register == Register.None)
            throw new CpuException("Missing opmask");

        return ProcessorRegisters.EvaluateK(register);
    }

    /// <summary>
    /// Invokes an interrupt. This is akin to the INT instruction.
    /// </summary>
    /// <param name="interruptNumber">The interrupt number.</param>
    /// <exception cref="NotImplementedException"></exception>
    public void RaiseInterrupt(byte interruptNumber)
    {
        if (IsRunningInProtectedMode)
        {
            uint targetIdtEntryAddress = interruptNumber * 8u;
            ReadOnlySpan<byte> idtEntry = Memory.Read16Bytes(targetIdtEntryAddress);
            Descriptor descriptor = MemoryMarshal.Cast<byte, Descriptor>(idtEntry)[0];
            GateDescriptor gate = GateDescriptor.FromBase(descriptor);

            ProcessorRegisters.Rip = ((gate.OffsetHigh | gate.OffsetLow) << 4) + gate.Selector;
        }
        else
        {
            throw new NotImplementedException("Invocation of interrupts in any other mode but protected mode is not implemented");
        }
    }

    /// <summary>
    /// Invokes interrupt of type 0x03, similar to a breakpoint.
    /// </summary>
    public void Break()
    {
        this.RaiseInterrupt(0x03);
    }

    /// <summary>
    /// Pushes a 64-bit unsigned integer onto the stack.
    /// </summary>
    /// <param name="value">64-bit unsigned integer.</param>
    public void StackPush(ulong value)
    {
        this.ProcessorRegisters.Rsp -= 8;
        this.Memory.WriteUInt64(this.ProcessorRegisters.Rsp, value);
    }

    /// <summary>
    /// Pushes a 32-bit unsigned integer onto the stack.
    /// </summary>
    /// <param name="value">32-bit unsigned integer.</param>
    public void StackPush(uint value)
    {
        this.ProcessorRegisters.Rsp -= 4;
        this.Memory.WriteUInt32(this.ProcessorRegisters.Rsp, value);
    }

    /// <summary>
    /// Pushes a 16-bit unsigned integer onto the stack.
    /// </summary>
    /// <param name="value">16-bit unsigned integer.</param>
    public void StackPush(ushort value)
    {
        this.ProcessorRegisters.Rsp -= 2;
        this.Memory.WriteUInt16(this.ProcessorRegisters.Rsp, value);
    }

    /// <summary>
    /// Pops a 64-bit unsigned integer from the stack.
    /// </summary>
    /// <returns>64-bit unsigned integer.</returns>
    public ulong StackPopU64()
    {
        ulong value = this.Memory.ReadUInt64(this.ProcessorRegisters.Rsp);
        this.ProcessorRegisters.Rsp += 8;
        return value;
    }

    /// <summary>
    /// Pops a 32-bit unsigned integer from the stack.
    /// </summary>
    /// <returns>32-bit unsigned integer.</returns>
    public uint StackPopU32()
    {
        uint value = this.Memory.ReadUInt32(this.ProcessorRegisters.Rsp);
        this.ProcessorRegisters.Rsp += 4;
        return value;
    }

    /// <summary>
    /// Pops a 16-bit unsigned integer from the stack.
    /// </summary>
    /// <returns>16-bit unsigned integer.</returns>
    public ushort StackPopU16()
    {
        ushort value = this.Memory.ReadUInt16(this.ProcessorRegisters.Rsp);
        this.ProcessorRegisters.Rsp += 2;
        return value;
    }

    /// <summary>
    /// Raises an exception.
    /// </summary>
    /// <param name="record">Information about the exception.</param>
    public void RaiseException(IErrorRecord record)
    {
        this.ProcessorRegisters.Rip = this.CurrentRip;
        this.StackPush(record.ErrorCode);
        this.RaiseInterrupt(record.Interrupt);
    }

    /// <summary>
    /// Raises the "Device Not Available" error.
    /// </summary>
    public void RaiseDeviceNotAvailable()
    {
        this.RaiseException(StaticErrors.DeviceUnavailable);
    }

    /// <summary>
    /// Raises an "Unbound Index" error.
    /// </summary>
    public void RaiseUnboundIndex()
    {
        this.RaiseException(StaticErrors.UnboundIndex);
    }

    private Vector64<ulong> GetVectorFromMMOrMemory64(in Instruction instruction, int operand)
    {
        return instruction.GetOpKind(operand) switch
        {
            OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand64(instruction)).As<float, ulong>(),
            OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(operand))),
            _ => Vector64<ulong>.Zero
        };
    }

    private Vector64<ulong> GetVectorFromMM(in Instruction instruction, int operand)
    {
        return Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(operand)));
    }

    private void WriteVector64ToMM<T>(in Instruction instruction, int operand, Vector64<T> value)
    {
        ulong valueToSet = value.As<T, ulong>().ToScalar();
        this.ProcessorRegisters.SetMM(instruction.GetOpRegister(operand), valueToSet);
    }

    private void AlterScalarOfXmm(Register xmm, float scalar)
    {
        Vector128<float> register = this.ProcessorRegisters.EvaluateXmm(xmm);
        register = register.WithElement(0, scalar);
        this.ProcessorRegisters.SetXmm(xmm, register);
    }

    private void AlterScalarOfXmm(Register xmm, int scalar)
    {
        Vector128<int> register = this.ProcessorRegisters.EvaluateXmm(xmm).As<float, int>();
        register = register.WithElement(0, scalar);
        this.ProcessorRegisters.SetXmm(xmm, register.As<int, float>());
    }

    private void AlterScalarOfXmm(Register xmm, uint scalar)
    {
        Vector128<uint> register = this.ProcessorRegisters.EvaluateXmm(xmm).As<float, uint>();
        register = register.WithElement(0, scalar);
        this.ProcessorRegisters.SetXmm(xmm, register.As<uint, float>());
    }

    private void AlterScalarOfXmm(Register xmm, ulong scalar)
    {
        Vector128<ulong> register = this.ProcessorRegisters.EvaluateXmm(xmm).As<float, ulong>();
        register = register.WithElement(0, scalar);
        this.ProcessorRegisters.SetXmm(xmm, register.As<ulong, float>());
    }

    private void AlterScalarOfXmm(Register xmm, double scalar)
    {
        Vector128<double> register = this.ProcessorRegisters.EvaluateXmm(xmm).As<float, double>();
        register = register.WithElement(0, scalar);
        this.ProcessorRegisters.SetXmm(xmm, register.As<double, float>());
    }

    private void AlterScalarOfXmm(Register xmm, Half scalar)
    {
        Vector128<Half> register = this.ProcessorRegisters.EvaluateXmm(xmm).As<float, Half>();
        register = register.WithElement(0, scalar);
        this.ProcessorRegisters.SetXmm(xmm, register.As<Half, float>());
    }

    private double ReadXmmScalarOrDouble(in Instruction instruction, int operand)
    {
        return instruction.GetOpKind(operand) switch
        {
            OpKind.Memory => this.Memory.ReadDouble(GetMemOperand64(instruction)),
            OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(operand)).AsDouble().ToScalar(),
            _ => 0d
        };
    }

    private float ReadXmmScalarOrSingle(in Instruction instruction, int operand)
    {
        return instruction.GetOpKind(operand) switch
        {
            OpKind.Memory => this.Memory.ReadSingle(GetMemOperand64(instruction)),
            OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(operand)).ToScalar(),
            _ => 0f
        };
    }

    private ulong ReadXmmScalarOrUInt64(in Instruction instruction, int operand)
    {
        return instruction.GetOpKind(operand) switch
        {
            OpKind.Memory => this.Memory.ReadUInt64(GetMemOperand64(instruction)),
            OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(operand)).AsUInt64().ToScalar(),
            _ => 0uL
        };
    }

    /// <summary>
    /// Reads from an I/O port.
    /// </summary>
    /// <param name="index">Index of the I/O port.</param>
    /// <returns>Value of the I/O port given by <paramref name="index"/>.</returns>
    public byte ReadIOPort(byte index)
    {
        return (byte)this.IOPorts[index].Read();
    }

    /// <summary>
    /// Reads from an I/O port.
    /// </summary>
    /// <param name="index">Index of the I/O port.</param>
    /// <returns>Value of the I/O port given by <paramref name="index"/>.</returns>
    public ushort ReadIOPort(ushort index)
    {
        return (ushort)this.IOPorts[index].Read();
    }

    /// <summary>
    /// Reads from an I/O port.
    /// </summary>
    /// <param name="index">Index of the I/O port.</param>
    /// <returns>Value of the I/O port given by <paramref name="index"/>.</returns>
    public uint ReadIOPort(uint index)
    {
        return (uint)this.IOPorts[index].Read();
    }

    /// <summary>
    /// Reads from an I/O port.
    /// </summary>
    /// <param name="index">Index of the I/O port.</param>
    /// <returns>Value of the I/O port given by <paramref name="index"/>.</returns>
    public ulong ReadIOPort(ulong index)
    {
        return this.IOPorts[index].Read();
    }

    /// <summary>
    /// Writes to an I/O port.
    /// </summary>
    /// <param name="port">Index of the port.</param>
    /// <param name="accumulator">Value to write to the port.</param>
    private void WriteIOPort(uint port, byte accumulator)
    {
        this.IOPorts[port].Write(accumulator);
    }

    /// <summary>
    /// Writes to an I/O port.
    /// </summary>
    /// <param name="port">Index of the port.</param>
    /// <param name="accumulator">Value to write to the port.</param>
    private void WriteIOPort(uint port, ushort accumulator)
    {
        this.IOPorts[port].Write(accumulator);
    }

    /// <summary>
    /// Writes to an I/O port.
    /// </summary>
    /// <param name="port">Index of the port.</param>
    /// <param name="accumulator">Value to write to the port.</param>
    private void WriteIOPort(uint port, uint accumulator)
    {
        this.IOPorts[port].Write(accumulator);
    }

    /// <summary>
    /// Writes to an I/O port.
    /// </summary>
    /// <param name="port">Index of the port.</param>
    /// <param name="accumulator">Value to write to the port.</param>
    private void WriteIOPort(uint port, ulong accumulator)
    {
        this.IOPorts[port].Write(accumulator);
    }

    private ulong NormalizeAddress(ulong address)
    {
        // Back in the 8086 days, address was calculated using (DS<<4)+address due
        // to memory limitations.
        if (this.ProcessorRegisters.RFlagsVM)
            return (ulong)(this.ProcessorRegisters.Ds << 4) + address;

        return address;
    }

    private Vector64<float> EvaluateMMOrMemoryAsVector64(in Instruction instruction, int operand)
    {
        OpKind kind = instruction.GetOpKind(operand);
        return kind switch
        {
            OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand64(instruction)),
            OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(operand))).AsSingle(),
            _ => Vector64<float>.Zero
        };
    }

    /// <summary>
    /// Raises the Undefined OpCode error (UD#), with interrupt vector 5.
    /// </summary>
    public void RaiseUndefinedOpCode()
    {
        this.RaiseException(StaticErrors.UndefinedOpCode);
    }

    private void EnsureFsGsBaseIsAllowed()
    {
        if (!this.ProcessorRegisters.CR4FSGSBASE)
        {
            this.RaiseUndefinedOpCode();
        }
    }
}
