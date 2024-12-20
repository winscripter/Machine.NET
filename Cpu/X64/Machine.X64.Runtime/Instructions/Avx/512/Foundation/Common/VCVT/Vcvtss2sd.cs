using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtss2sd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtss2sd_xmm_k1z_xmm_xmmm32_sae:
                {
                    float scalar = ReadXmmScalarOrSingle(in instruction, 2);
                    Vector128<double> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsDouble();
                    xmm = xmm.WithElement(0, (double)scalar);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm.AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
