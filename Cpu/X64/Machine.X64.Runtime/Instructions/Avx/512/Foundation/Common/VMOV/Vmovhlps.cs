using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovhlps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovhlps_xmm_xmm_xmm:
                {
                    Vector128<float> xmm1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> xmm2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2));
                    Vector128<float> result = xmm1.WithLower(xmm2.GetUpper());
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
