using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime.Core.Avx512;

internal static class NewOpmaskCore
{
    private static void ImplementKRegisterLeftShift(ProcessorRegisters registers, ulong number, byte shiftBy, int bitness, Register output)
    {
        ulong last = number;

        for (int i = 0; i < shiftBy; i++)
        {
            last = BitUtilities.LeftShiftFirstNBits(last, bitness);
        }

        registers.SetK(output, last);
    }

    public static void KLeftShiftInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterLeftShift(
            registers,
            number: registers.EvaluateK(instruction.GetOpRegister(1)),
            shiftBy: (byte)instruction.GetImmediate(2),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterRightShift(ProcessorRegisters registers, ulong number, byte shiftBy, int bitness, Register output)
    {
        ulong last = number;

        for (int i = 0; i < shiftBy; i++)
        {
            last = BitUtilities.RightShiftFirstNBits(last, bitness);
        }

        registers.SetK(output, last);
    }

    public static void KRightShiftInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterRightShift(
            registers,
            number: registers.EvaluateK(instruction.GetOpRegister(1)),
            shiftBy: (byte)instruction.GetImmediate(2),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterOr(ProcessorRegisters registers, ulong k2, ulong k3, int bitness, Register output)
    {
        ulong result = 0;
        for (int i = 0; i < bitness; i++)
        {
            BitUtilities.SetBit(ref result, i, BitUtilities.IsBitSet(k2, i) | BitUtilities.IsBitSet(k3, i));
        }
        registers.SetK(output, result);
    }

    public static void KorInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterOr(
            registers,
            k2: registers.EvaluateK(instruction.GetOpRegister(1)),
            k3: registers.EvaluateK(instruction.GetOpRegister(2)),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterAdd(ProcessorRegisters registers, ulong k2, ulong k3, int bitness, Register output)
    {
        ulong result = BitUtilities.AddFirstNBits(k2, k3, bitness);
        registers.SetK(output, result);
    }

    public static void KAddInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterAdd(
            registers,
            k2: registers.EvaluateK(instruction.GetOpRegister(1)),
            k3: registers.EvaluateK(instruction.GetOpRegister(2)),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterXor(ProcessorRegisters registers, ulong k2, ulong k3, int bitness, Register output)
    {
        ulong result = 0;
        for (int i = 0; i < bitness; i++)
        {
            BitUtilities.SetBit(ref result, i, BitUtilities.IsBitSet(k2, i) ^ BitUtilities.IsBitSet(k3, i));
        }
        registers.SetK(output, result);
    }

    public static void KXorInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterXor(
            registers,
            k2: registers.EvaluateK(instruction.GetOpRegister(1)),
            k3: registers.EvaluateK(instruction.GetOpRegister(2)),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterXnor(ProcessorRegisters registers, ulong k2, ulong k3, int bitness, Register output)
    {
        ulong result = 0;
        for (int i = 0; i < bitness; i++)
        {
            BitUtilities.SetBit(ref result, i, !(BitUtilities.IsBitSet(k2, i) ^ BitUtilities.IsBitSet(k3, i)));
        }
        registers.SetK(output, result);
    }

    public static void KXnorInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterXnor(
            registers,
            k2: registers.EvaluateK(instruction.GetOpRegister(1)),
            k3: registers.EvaluateK(instruction.GetOpRegister(2)),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterAnd(ProcessorRegisters registers, ulong k2, ulong k3, int bitness, Register output)
    {
        ulong result = 0;
        for (int i = 0; i < bitness; i++)
        {
            BitUtilities.SetBit(ref result, i, BitUtilities.IsBitSet(k2, i) & BitUtilities.IsBitSet(k3, i));
        }
        registers.SetK(output, result);
    }

    public static void KAndInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterAnd(
            registers,
            k2: registers.EvaluateK(instruction.GetOpRegister(1)),
            k3: registers.EvaluateK(instruction.GetOpRegister(2)),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }

    private static void ImplementKRegisterAndn(ProcessorRegisters registers, ulong k2, ulong k3, int bitness, Register output)
    {
        ulong result = 0;
        for (int i = 0; i < bitness; i++)
        {
            BitUtilities.SetBit(ref result, i, !(BitUtilities.IsBitSet(k2, i) & BitUtilities.IsBitSet(k3, i)));
        }
        registers.SetK(output, result);
    }

    public static void KAndnInstructionMain(ProcessorRegisters registers, in Instruction instruction, Code code, int bitness)
    {
        if (instruction.Code != code)
        {
            throw new CpuException($"Invalid or unrecognized code {instruction.Code} under mnemonic {instruction.Mnemonic}");
        }
        ImplementKRegisterAndn(
            registers,
            k2: registers.EvaluateK(instruction.GetOpRegister(1)),
            k3: registers.EvaluateK(instruction.GetOpRegister(2)),
            bitness: bitness,
            output: instruction.GetOpRegister(0)
        );
    }
}
