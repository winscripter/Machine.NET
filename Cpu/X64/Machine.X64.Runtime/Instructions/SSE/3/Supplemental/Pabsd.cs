using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pabsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pabsd_mm_mmm64:
                {
                    Vector64<int> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, int>();

                    Vector64<int> result = Vector64<int>.Zero;
                    for (int i = 0; i < Vector64<int>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Abs(src[i]));
                    }

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<int, ulong>()[0]);
                    break;
                }

            case Code.Pabsd_xmm_xmmm128:
                {
                    Vector128<int> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, int>();

                    Vector128<int> result = Vector128<int>.Zero;
                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Abs(src[i]));
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
