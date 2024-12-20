using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrsqrt28ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrsqrt28ps_zmm_k1z_zmmm512b32_sae:
                {
                    Vector512<float> v512 = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> result = Vector512<float>.Zero;

                    for (int i = 0; i < Vector512<float>.Count; i++)
                        result = result.WithElement(i, MathF.ReciprocalSqrtEstimate(v512[i]));

                    result = result.K1z(0F, this.ProcessorRegisters.K1);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
