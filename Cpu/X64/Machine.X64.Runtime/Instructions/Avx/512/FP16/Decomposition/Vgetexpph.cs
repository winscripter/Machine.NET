using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vgetexpph(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vgetexpph_xmm_k1z_xmmm128b16:
                {
                    Vector128<Half> vec = EvaluateXmmFromInstruction(in instruction, 1).AsHalf();
                    
                    Vector128<Half> result = Vector128<Half>.Zero;
                    for (int i = 0; i < Vector128<Half>.Count; i++)
                        result = result.WithElement(i, (Half)RealHelpers.GetExponent(vec[i]));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            case Code.EVEX_Vgetexpph_ymm_k1z_ymmm256b16:
                {
                    Vector256<Half> vec = EvaluateYmmFromInstruction(in instruction, 1).AsHalf();

                    Vector256<Half> result = Vector256<Half>.Zero;
                    for (int i = 0; i < Vector256<Half>.Count; i++)
                        result = result.WithElement(i, (Half)RealHelpers.GetExponent(vec[i]));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            case Code.EVEX_Vgetexpph_zmm_k1z_zmmm512b16_sae:
                {
                    Vector512<Half> vec = EvaluateZmmFromInstruction(in instruction, 1).AsHalf();

                    Vector512<Half> result = Vector512<Half>.Zero;
                    for (int i = 0; i < Vector512<Half>.Count; i++)
                        result = result.WithElement(i, (Half)RealHelpers.GetExponent(vec[i]));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
