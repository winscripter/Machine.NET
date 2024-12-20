using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pcmpgtd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pcmpgtd_mm_mmm64:
                {
                    Vector64<uint> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, uint>();
                    Vector64<uint> dst = GetVectorFromMM(in instruction, 1).As<ulong, uint>();

                    Vector64<uint> result = Vector64<uint>.Zero;
                    for (int i = 0; i < Vector64<uint>.Count; i++)
                    {
                        result = result.WithElement(i, src[i] > dst[i] ? uint.MaxValue : (uint)0x00);
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Pcmpgtd_xmm_xmmm128:
                {
                    Vector128<uint> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();

                    Vector128<uint> result = Vector128<uint>.Zero;
                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        result = result.WithElement(i, src[i] > dst[i] ? uint.MaxValue : (uint)0x00);
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
