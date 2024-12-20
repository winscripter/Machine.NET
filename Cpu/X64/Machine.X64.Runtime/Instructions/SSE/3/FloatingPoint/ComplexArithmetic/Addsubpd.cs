using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void addsubpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Addsubpd_xmm_xmmm128:
                {
                    Vector128<double> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();

                    Vector128<double> result = Vector128.Create(
                        src[0] - dst[0],
                        src[1] + dst[1]
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
