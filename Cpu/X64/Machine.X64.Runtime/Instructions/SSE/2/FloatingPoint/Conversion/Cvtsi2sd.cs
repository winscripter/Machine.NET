using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtsi2sd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtsi2sd_xmm_rm32:
                {
                    uint doubleWord = RMEvaluate32(in instruction, 1);
                    Vector128<double> outputVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    outputVector = outputVector.WithElement(0, (double)doubleWord);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), outputVector.As<double, float>());
                    break;
                }

            case Code.Cvtsi2sd_xmm_rm64:
                {
                    ulong quadWord = RMEvaluate64(in instruction, 1);
                    Vector128<double> outputVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    outputVector = outputVector.WithElement(0, quadWord);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), outputVector.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
