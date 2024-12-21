using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void maxss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Maxss_xmm_xmmm32:
                {
                    float scalar2 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar(),
                        _ => float.NaN
                    };
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    float scalar1 = parameter1.ToScalar();

                    float scalarResult = scalar1 > scalar2 ? scalar1 : scalar2;
                    parameter1 = parameter1.WithElement(0, scalarResult);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter1);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
