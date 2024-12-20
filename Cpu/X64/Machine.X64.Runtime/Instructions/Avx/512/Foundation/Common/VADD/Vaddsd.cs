using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vaddsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vaddsd_xmm_k1z_xmm_xmmm64_er:
                {
                    Vector128<double> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> b = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, double>();

                    Vector128<double> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    result = result.WithElement(0, a[0] + b[0]);

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0d, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
