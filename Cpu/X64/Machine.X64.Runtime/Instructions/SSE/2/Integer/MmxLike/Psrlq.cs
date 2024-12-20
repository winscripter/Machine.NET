using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psrlq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psrlq_mm_imm8:
                {
                    Vector64<ulong> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, ulong>();
                    byte shiftBy = (byte)instruction.GetImmediate(1);

                    Vector64<ulong> result = Vector64<ulong>.Zero;
                    for (int i = 0; i < Vector64<ulong>.Count; i++)
                    {
                        result = result.WithElement(i, (ulong)(src[i] >> shiftBy));
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Psrlq_mm_mmm64:
                {
                    Vector64<ulong> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, ulong>();
                    Vector64<ulong> dst = GetVectorFromMM(in instruction, 1).As<ulong, ulong>();

                    Vector64<ulong> result = Vector64<ulong>.Zero;
                    for (int i = 0; i < Vector64<ulong>.Count; i++)
                    {
                        result = result.WithElement(i, (ulong)(src[i] >> (ushort)dst[i]));
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Psrlq_xmm_imm8:
                {
                    Vector128<ulong> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    byte imm = (byte)instruction.GetImmediate(1);

                    Vector128<ulong> result = Vector128<ulong>.Zero;
                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        result = result.WithElement(i, (ulong)(src[i] >> imm));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.Psrlq_xmm_xmmm128:
                {
                    Vector128<ulong> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();

                    Vector128<ulong> result = Vector128<ulong>.Zero;
                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        result = result.WithElement(i, (ulong)(src[i] >> (ushort)dst[i]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
