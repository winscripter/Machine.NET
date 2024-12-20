using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vgetexpps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vgetexpps_xmm_k1z_xmmm128b32:
                {
                    Vector128<float> input = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = Vector128<float>.Zero;

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        result = result.WithElement(i, RealHelpers.BreakDownFloatingPointInteger(input[i]).exponent);
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vgetexpps_ymm_k1z_ymmm256b32:
                {
                    Vector256<float> input = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> result = Vector256<float>.Zero;

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        result = result.WithElement(i, RealHelpers.BreakDownFloatingPointInteger(input[i]).exponent);
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vgetexpps_zmm_k1z_zmmm512b32_sae:
                {
                    Vector512<float> input = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> result = Vector512<float>.Zero;

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        result = result.WithElement(i, RealHelpers.BreakDownFloatingPointInteger(input[i]).exponent);
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
