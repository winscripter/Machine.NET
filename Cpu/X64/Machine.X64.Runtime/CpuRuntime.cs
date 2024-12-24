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
public sealed partial class CpuRuntime(int memorySize = 65536, int ioPortCount = 256, int bitness = 64)
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

    private ulong GetMemOperand(in Instruction instruction)
    {
        if (instruction.MemoryBase != Register.None)
        {
            if (instruction.MemoryIndex != Register.None)
            {
                if (this.ProcessorRegisters.RFlagsVM)
                {
                    // In Virtual 8086 Mode, memory base & index is always 16-bit.
                    return (ulong)(this.ProcessorRegisters.Ds << 4) +
                        (ulong)this.ProcessorRegisters.EvaluateRegisterValue16(instruction.MemoryBase) +
                        this.ProcessorRegisters.EvaluateRegisterValue16(instruction.MemoryIndex);
                }
                else
                {
                    return EvaluateGpr(instruction.MemoryBase) + EvaluateGpr(instruction.MemoryIndex);
                }
            }
            else
            {
                if (this.ProcessorRegisters.RFlagsVM)
                {
                    // In Virtual 8086 Mode, memory base & index is always 16-bit.
                    return (ulong)(this.ProcessorRegisters.Ds << 4) + (ulong)this.ProcessorRegisters.EvaluateRegisterValue16(instruction.MemoryBase);
                }
                else
                {
                    return EvaluateGpr(instruction.MemoryBase);
                }
            }
        }
        else
        {
            if (instruction.IsIPRelativeMemoryOperand)
            {
                return this.ProcessorRegisters.Rip +
                    (instruction.MemoryDisplSize == 4 ? instruction.MemoryDisplacement32 : instruction.MemoryDisplacement64);
            }
            else
            {
                return instruction.MemoryDisplSize == 4 ? instruction.MemoryDisplacement32 : instruction.MemoryDisplacement64;
            }
        }
    }

    private ulong EvaluateGpr(Register register)
    {
        if (register is Register.AH or Register.AL or Register.BH or Register.BL or Register.CH or Register.CL or Register.DH or Register.DL)
        {
            // 8 bit register
            return this.ProcessorRegisters.EvaluateRegisterValue8(register);
        }
        else if (register is Register.AX or Register.BX or Register.CX or Register.DX or Register.SI or Register.DI or Register.CS or Register.DS or Register.SS or Register.ES or Register.BP)
        {
            // 16 bit register
            return this.ProcessorRegisters.EvaluateRegisterValue16(register);
        }
        else if (register is Register.EAX or Register.EBX or Register.ECX or Register.EDX or Register.ESI or Register.EDI or Register.EIP or Register.EBP)
        {
            // 32 bit register
            return this.ProcessorRegisters.EvaluateRegisterValue32(register);
        }
        else if (register is Register.RAX or Register.RBX or Register.RCX or Register.RDX or Register.RSI or Register.RDI or Register.RIP or Register.RBP or Register.FS or Register.GS)
        {
            // 64 bit register
            return this.ProcessorRegisters.EvaluateRegisterValue64(register);
        }
        return 0uL;
    }

    private void RMSet8(in Instruction instruction, byte value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand(in instruction), value);
        else
            ProcessorRegisters.WriteToRegister8(instruction.GetOpRegister(registerPositionCase), value);
    }

    private void RMSet16(in Instruction instruction, ushort value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand(in instruction), value);
        else
            ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(registerPositionCase), value);
    }

    private void RMSet32(in Instruction instruction, uint value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand(in instruction), value);
        else
            ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(registerPositionCase), value);
    }

    private void RMSet64(in Instruction instruction, ulong value, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            AlterObjectAtMemory(GetMemOperand(in instruction), value);
        else
            ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(registerPositionCase), value);
    }

    private byte RMEvaluate8(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory8(GetMemOperand(in instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(registerPositionCase));
    }

    private ushort RMEvaluate16(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory16(GetMemOperand(in instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(registerPositionCase));
    }

    private uint RMEvaluate32(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory32(GetMemOperand(in instruction));
        else
            return ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(registerPositionCase));
    }

    private ulong RMEvaluate64(in Instruction instruction, int registerPositionCase = 0)
    {
        if (IsRMMemory(in instruction))
            return QueryObjectAtMemory64(GetMemOperand(in instruction));
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
            value2 = this.Memory.ReadBinaryVector128(GetMemOperand(in instruction));
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
            value2 = this.Memory.ReadBinaryVector256(GetMemOperand(in instruction));
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
            value2 = this.Memory.ReadBinaryVector512(GetMemOperand(in instruction));
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
            OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand(in instruction)).As<float, ulong>(),
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
            OpKind.Memory => this.Memory.ReadDouble(GetMemOperand(in instruction)),
            OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(operand)).AsDouble().ToScalar(),
            _ => 0d
        };
    }

    private float ReadXmmScalarOrSingle(in Instruction instruction, int operand)
    {
        return instruction.GetOpKind(operand) switch
        {
            OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
            OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(operand)).ToScalar(),
            _ => 0f
        };
    }

    private ulong ReadXmmScalarOrUInt64(in Instruction instruction, int operand)
    {
        return instruction.GetOpKind(operand) switch
        {
            OpKind.Memory => this.Memory.ReadUInt64(GetMemOperand(in instruction)),
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
            OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand(in instruction)),
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

    private bool EnsureFsGsBaseIsAllowed()
    {
        if (!this.ProcessorRegisters.CR4FSGSBASE)
        {
            this.RaiseUndefinedOpCode();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Runs instruction in memory.
    /// </summary>
    /// <param name="physicalAddress">Address.</param>
    public void RunInMemory(ulong physicalAddress)
    {
        // WARNING: This is definitely not efficient and I'm looking for a better solution.
        // https://github.com/icedland/iced/discussions/649

        using var ms = new MemoryStream();
        for (ulong i = 0; i < 15; i++) // Just to be safe
        {
            ms.WriteByte(this.Memory[physicalAddress + i]);
        }
        ms.Position = 0;
        var decoder = Decoder.Create(bitness, new StreamCodeReader(ms));
        decoder.IP = 0;
        decoder.Decode(out Instruction instruction);
        
        if (decoder.LastError == DecoderError.InvalidInstruction)
        {
            throw new InvalidOperationException($"An invalid instruction has been detected. CS: {this.ProcessorRegisters.Cs} RIP: {this.ProcessorRegisters.Rip}");
        }
        Run(in instruction);

        if (instruction.FlowControl == FlowControl.Next)
            this.ProcessorRegisters.Rip += (ulong)instruction.Length;
    }

    public void Run(int numberOfTimes)
    {
        for (int i = 0; i < numberOfTimes; i++)
        {
            RunInMemory((ulong)(this.ProcessorRegisters.Cs << 4) + this.ProcessorRegisters.Rip);
        }
    }

    public void RunUntilTrue(Func<bool> until)
    {
        while (!until())
        {
            RunInMemory((ulong)(this.ProcessorRegisters.Cs << 4) + this.ProcessorRegisters.Rip);
        }
    }

    public void RunUntilNotBusyOrTrue(Func<bool> until)
    {
        if (Busy)
        {
            return;
        }
        Busy = true;

        while (Busy && !until())
        {
            RunInMemory((ulong)(this.ProcessorRegisters.Cs << 4) + this.ProcessorRegisters.Rip);
        }
    }

    public void RunUntilNotBusy()
    {
        if (Busy)
        {
            return;
        }
        Busy = true;

        while (Busy)
        {
            RunInMemory((ulong)(this.ProcessorRegisters.Cs << 4) + this.ProcessorRegisters.Rip);
        }
    }

    public ulong VirtualAddress
    {
        get
        {
            if (this.ProcessorRegisters.RFlagsVM)
                return (ulong)(this.ProcessorRegisters.Cs << 4) + this.ProcessorRegisters.Rip;
            return this.ProcessorRegisters.Rip;
        }
    }

    public bool RunUntilNotBusy(int maxLimit)
    {
        if (Busy)
        {
            return false;
        }
        Busy = true;

        int count = 0;
        while (Busy)
        {
            if (count > maxLimit)
            {
                Busy = false;
                return false;
            }
            RunInMemory((ulong)(this.ProcessorRegisters.Cs << 4) + this.ProcessorRegisters.Rip);
            count++;
        }
        return true;
    }

    public void LoadProgram(byte[] program, ulong at)
    {
        for (int i = 0; i < program.Length; i++)
        {
            this.Memory[at + (ulong)i] = program[i];
        }
    }

    public void SetRsp(ulong rsp) => this.ProcessorRegisters.Rsp = rsp;

    /// <summary>
    /// Tells Machine.NET to use 8086 compatibility mode.
    /// </summary>
    public void Use8086Compatibility() => Use8086Compatibility(true);

    /// <summary>
    /// Tells Machine.NET to use 8086 compatibility mode.
    /// </summary>
    /// <param name="use8086Compatibility"></param>
    public void Use8086Compatibility(bool use8086Compatibility) => this.ProcessorRegisters.RFlagsVM = use8086Compatibility;
}
