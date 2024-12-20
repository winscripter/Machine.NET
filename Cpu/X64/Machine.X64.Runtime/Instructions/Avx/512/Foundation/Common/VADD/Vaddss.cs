using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vaddss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vaddss_xmm_k1z_xmm_xmmm32_er:
                {
                    Vector128<float> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> b = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2));

                    Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    result = result.WithElement(0, a[0] + b[0]);

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0f, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
