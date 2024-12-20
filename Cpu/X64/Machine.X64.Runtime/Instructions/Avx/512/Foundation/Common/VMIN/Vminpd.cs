using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vminpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vminpd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<double> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    Vector128<double> parameter2 = EvaluateXmmFromInstruction(in instruction, 1).AsDouble();
                    Vector128<double> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vminpd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<double> parameter1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, double>();
                    Vector256<double> parameter2 = EvaluateYmmFromInstruction(in instruction, 1).AsDouble();
                    Vector256<double> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vminpd_zmm_k1z_zmm_zmmm512b64_sae:
                {
                    Vector512<double> parameter1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, double>();
                    Vector512<double> parameter2 = EvaluateZmmFromInstruction(in instruction, 1).AsDouble();
                    Vector512<double> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
