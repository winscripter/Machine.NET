using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtsi2ss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtsi2ss_xmm_rm32:
                {
                    float value = RMEvaluate32(in instruction, 1);
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    xmm = xmm.WithElement(0, value); // effectively changes scalar value
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            case Code.Cvtsi2ss_xmm_rm64:
                {
                    float value = RMEvaluate64(in instruction, 1);
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    xmm = xmm.WithElement(0, value); // effectively changes scalar value
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
