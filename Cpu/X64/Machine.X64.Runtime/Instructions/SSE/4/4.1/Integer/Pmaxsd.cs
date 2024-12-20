using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmaxsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmaxsd_xmm_xmmm128:
                {
                    Vector128<int> src = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, int>();
                    Vector128<int> dst = EvaluateXmmFromInstruction(in instruction, 1).As<float, int>();

                    Vector128<int> result = Vector128<int>.Zero;
                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Max(src[i], dst[i]));
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
