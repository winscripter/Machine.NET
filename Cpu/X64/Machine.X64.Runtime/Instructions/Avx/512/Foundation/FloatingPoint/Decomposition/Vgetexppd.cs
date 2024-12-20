using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vgetexppd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vgetexppd_xmm_k1z_xmmm128b64:
                {
                    Vector128<double> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> result = Vector128<double>.Zero;

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        result = result.WithElement(i, RealHelpers.BreakDownFloatingPointInteger(input[i]).exponent);
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vgetexppd_ymm_k1z_ymmm256b64:
                {
                    Vector256<double> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector256<double> result = Vector256<double>.Zero;

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        result = result.WithElement(i, RealHelpers.BreakDownFloatingPointInteger(input[i]).exponent);
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vgetexppd_zmm_k1z_zmmm512b64_sae:
                {
                    Vector512<double> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<double> result = Vector512<double>.Zero;

                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        result = result.WithElement(i, RealHelpers.BreakDownFloatingPointInteger(input[i]).exponent);
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
