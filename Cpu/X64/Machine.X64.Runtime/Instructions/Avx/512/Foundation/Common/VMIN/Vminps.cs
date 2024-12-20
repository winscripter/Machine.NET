using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vminps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vminps_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> parameter2 = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vminps_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<float> parameter1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0));
                    Vector256<float> parameter2 = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vminps_zmm_k1z_zmm_zmmm512b32_sae:
                {
                    Vector512<float> parameter1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));
                    Vector512<float> parameter2 = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
