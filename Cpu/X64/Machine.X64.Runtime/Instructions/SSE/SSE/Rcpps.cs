using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rcpps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rcpps_xmm_xmmm128:
                {
                    Vector128<float> parameter2 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)),
                        _ => Vector128<float>.Zero
                    };
                    parameter2 = parameter2.WithElement(0, MathF.ReciprocalEstimate(parameter2[0]))
                                           .WithElement(1, MathF.ReciprocalEstimate(parameter2[1]))
                                           .WithElement(2, MathF.ReciprocalEstimate(parameter2[2]))
                                           .WithElement(3, MathF.ReciprocalEstimate(parameter2[3]));
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter2);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
