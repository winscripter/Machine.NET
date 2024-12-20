using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vdivss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vdivss_xmm_k1z_xmm_xmmm32_er:
                {
                    float scalar = ReadXmmScalarOrSingle(in instruction, 2);
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    xmm = xmm.WithElement(0, xmm.ToScalar() / scalar);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
