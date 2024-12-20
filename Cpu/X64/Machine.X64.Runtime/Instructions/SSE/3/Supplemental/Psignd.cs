using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psignd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psignd_mm_mmm64:
                {
                    Vector64<int> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, int>();
                    Vector64<int> dst = GetVectorFromMM(in instruction, 0).As<ulong, int>();

                    Vector64<int> result = Vector64<int>.Zero;
                    for (int i = 0; i < Vector64<int>.Count; i++)
                    {
                        result = result.WithElement(i, dst[i] < 0 ? (int)-src[i] : src[i]);
                    }

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<int, ulong>()[0]);
                    break;
                }

            case Code.Psignd_xmm_xmmm128:
                {
                    Vector128<int> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, int>();
                    Vector128<int> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, int>();

                    Vector128<int> result = Vector128<int>.Zero;
                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        result = result.WithElement(i, dst[i] < 0 ? (int)-src[i] : src[i]);
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<int, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
