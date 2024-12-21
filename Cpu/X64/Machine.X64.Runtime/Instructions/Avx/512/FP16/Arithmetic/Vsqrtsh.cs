using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vsqrtsh(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vsqrtsh_xmm_k1z_xmm_xmmm16_er:
                {
                    Half scalar = instruction.GetOpKind(2) switch
                    {
                        OpKind.Memory => this.Memory.ReadHalf(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).AsHalf().ToScalar(),
                        _ => Half.Zero
                    };
                    Vector128<Half> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, Half>();
                    vec = vec.WithElement(0, Half.Sqrt(scalar));

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        vec = vec.K1z(Half.Zero, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
