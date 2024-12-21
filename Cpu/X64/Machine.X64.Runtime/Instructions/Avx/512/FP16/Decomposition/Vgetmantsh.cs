using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vgetmantsh(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vgetmantsh_xmm_k1z_xmm_xmmm16_imm8_sae:
                {
                    Vector128<Half> vec = EvaluateXmmFromInstruction(in instruction, 1).AsHalf();
                    Half scalar = instruction.GetOpKind(2) switch
                    {
                        OpKind.Memory => this.Memory.ReadHalf(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).AsHalf().ToScalar(),
                        _ => Half.Zero
                    };

                    vec = vec.WithElement(0, (Half)RealHelpers.GetMantissa(scalar));
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
