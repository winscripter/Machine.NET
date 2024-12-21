using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rsqrtss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rsqrtss_xmm_xmmm32:
                {
                    Vector128<float> parameter2 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)),
                        _ => Vector128<float>.Zero
                    };
                    parameter2 = parameter2.WithElement(0, MathF.ReciprocalSqrtEstimate(parameter2.ToScalar()));
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter2);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
