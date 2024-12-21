using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void comiss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Comiss_xmm_xmmm32:
                {
                    float valueToCompareFrom = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).ToScalar();
                    float valueToCompareWith = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.RFlagsZF = valueToCompareFrom == valueToCompareWith;
                    this.ProcessorRegisters.RFlagsPF = float.IsNaN(valueToCompareFrom) || float.IsNaN(valueToCompareWith);
                    this.ProcessorRegisters.RFlagsCF = valueToCompareFrom < valueToCompareWith;

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
