using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvttss2si(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvttss2si_r32_xmmm32:
                {
                    float valueToConvert = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)MathF.Truncate(valueToConvert));
                    break;
                }

            case Code.Cvttss2si_r64_xmmm32:
                {
                    float valueToConvert = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (uint)MathF.Truncate(valueToConvert));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
