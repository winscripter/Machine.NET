using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psrld(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psrld_mm_imm8:
                {
                    Vector64<uint> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, uint>();
                    byte shiftBy = (byte)instruction.GetImmediate(1);

                    Vector64<uint> result = Vector64<uint>.Zero;
                    for (int i = 0; i < Vector64<uint>.Count; i++)
                    {
                        result = result.WithElement(i, (uint)(src[i] >> shiftBy));
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Psrld_mm_mmm64:
                {
                    Vector64<uint> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, uint>();
                    Vector64<uint> dst = GetVectorFromMM(in instruction, 1).As<ulong, uint>();

                    Vector64<uint> result = Vector64<uint>.Zero;
                    for (int i = 0; i < Vector64<uint>.Count; i++)
                    {
                        result = result.WithElement(i, (uint)(src[i] >> (ushort)dst[i]));
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Psrld_xmm_imm8:
                {
                    Vector128<uint> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    byte imm = (byte)instruction.GetImmediate(1);

                    Vector128<uint> result = Vector128<uint>.Zero;
                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        result = result.WithElement(i, (uint)(src[i] >> imm));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.Psrld_xmm_xmmm128:
                {
                    Vector128<uint> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();

                    Vector128<uint> result = Vector128<uint>.Zero;
                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        result = result.WithElement(i, (uint)(src[i] >> (ushort)dst[i]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
