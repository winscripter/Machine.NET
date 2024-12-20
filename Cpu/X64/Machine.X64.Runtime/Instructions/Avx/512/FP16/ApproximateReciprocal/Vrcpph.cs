using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrcpph(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrcpph_xmm_k1z_xmmm128b16:
                {
                    Vector128<Half> vec = EvaluateXmmFromInstruction(in instruction, 1).AsHalf();
                    Vector128<Half> result = Vector128<Half>.Zero;
                    for (int i = 0; i < Vector128<Half>.Count; i++)
                    {
                        result = result.WithElement(i, Half.ReciprocalEstimate(vec[i]));
                    }

                    result = result.K1z(Half.Zero, this.ProcessorRegisters.K1);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
