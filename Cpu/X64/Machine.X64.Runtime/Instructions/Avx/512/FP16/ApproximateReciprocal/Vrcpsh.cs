using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrcpsh(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrcpsh_xmm_k1z_xmm_xmmm16:
                {
                    Vector128<Half> vec = EvaluateXmmFromInstruction(in instruction, 1).AsHalf();

                    Vector128<Half> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsHalf();
                    result = result.WithElement(0, Half.ReciprocalEstimate(vec[0]));
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
