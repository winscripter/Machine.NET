using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrsqrt28ss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrsqrt28ss_xmm_k1z_xmm_xmmm32_sae:
                {
                    Vector128<float> v128 = EvaluateXmmFromInstruction(in instruction, 1);
                    float scalar = instruction.GetOpKind(2) switch
                    {
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).ToScalar(),
                        OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
                        _ => 0F
                    };

                    v128 = v128.WithElement(0, MathF.ReciprocalSqrtEstimate(scalar));
                    v128 = v128.K1z(0F, this.ProcessorRegisters.K1);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), v128);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
