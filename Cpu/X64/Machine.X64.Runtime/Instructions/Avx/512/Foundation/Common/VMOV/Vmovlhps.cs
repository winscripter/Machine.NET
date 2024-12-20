using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovlhps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovlhps_xmm_xmm_xmm:
                {
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> parameter2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2));

                    parameter1 = parameter1.WithUpper(parameter2.GetLower());

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter1);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
