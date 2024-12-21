using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtss2sd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtss2sd_xmm_xmmm32:
                {
                    float scalarSinglePrecision = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadSingle(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).ToScalar(),
                        _ => 0F
                    };
                    Vector128<double> outputVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    outputVector = outputVector.WithElement(0, (double)scalarSinglePrecision);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), outputVector.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
