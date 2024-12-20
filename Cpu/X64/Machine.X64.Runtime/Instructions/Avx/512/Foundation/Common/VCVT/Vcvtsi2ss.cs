using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtsi2ss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtsi2ss_xmm_xmm_rm32_er:
                {
                    uint scalar = RMEvaluate32(in instruction, 2);
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    xmm = xmm.WithElement(0, scalar);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            case Code.EVEX_Vcvtsi2ss_xmm_xmm_rm64_er:
                {
                    ulong scalar = RMEvaluate64(in instruction, 2);
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    xmm = xmm.WithElement(0, scalar);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
