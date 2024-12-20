using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void shl(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Shl_rm16_1:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) << 1);
                    ProcessFlags(result);
                    RMSet16(in instruction, (ushort)result, 0);
                    break;
                }

            case Code.Shl_rm16_CL:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) << ProcessorRegisters.Cl);
                    ProcessFlags(result);
                    RMSet16(in instruction, (ushort)result, 0);
                    break;
                }

            case Code.Shl_rm16_imm8:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) << (byte)instruction.GetImmediate(1));
                    ProcessFlags(result);
                    RMSet16(in instruction, (ushort)result, 0);
                    break;
                }

            case Code.Shl_rm32_1:
                {
                    ulong result = (ulong)((ulong)RMEvaluate32(in instruction, 0) << 1);
                    ProcessFlags(result);
                    RMSet32(in instruction, (uint)result, 0);
                    break;
                }

            case Code.Shl_rm32_CL:
                {
                    ulong result = (ulong)((ulong)RMEvaluate32(in instruction, 0) << ProcessorRegisters.Al);
                    ProcessFlags(result);
                    RMSet32(in instruction, (uint)result, 0);
                    break;
                }

            case Code.Shl_rm32_imm8:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) << (byte)instruction.GetImmediate(1));
                    ProcessFlags(result);
                    RMSet16(in instruction, (ushort)result, 0);
                    break;
                }

            case Code.Shl_rm64_1:
                {
                    ushort result = (ushort)((ushort)ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(0)) << RMEvaluate8(in instruction, 1));
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister8(instruction.GetOpRegister(0), (byte)result);
                    break;
                }

            case Code.Shl_rm64_CL:
                {
                    Int128 result = (Int128)((Int128)ProcessorRegisters.Rax << ProcessorRegisters.Cl);
                    ProcessFlags(result);
                    RMSet64(in instruction, (uint)result, 0);
                    break;
                }

            case Code.Shl_rm64_imm8:
                {
                    Int128 result = (Int128)((Int128)ProcessorRegisters.Rax << (byte)instruction.GetImmediate(1));
                    ProcessFlags(result);
                    RMSet64(in instruction, (uint)result, 0);
                    break;
                }

            case Code.Shl_rm8_1:
                {
                    ushort result = (ushort)((ushort)RMEvaluate8(in instruction, 0) << 1);
                    ProcessFlags(result);
                    RMSet8(in instruction, (byte)result, 0);
                    break;
                }

            case Code.Shl_rm8_CL:
                {
                    ushort result = (ushort)((ushort)RMEvaluate8(in instruction, 0) << ProcessorRegisters.Cl);
                    ProcessFlags(result);
                    RMSet8(in instruction, (byte)result, 0);
                    break;
                }

            case Code.Shl_rm8_imm8:
                {
                    ushort result = (ushort)((ushort)RMEvaluate8(in instruction, 0) << (byte)instruction.GetImmediate(1));
                    ProcessFlags(result);
                    RMSet8(in instruction, (byte)result, 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
