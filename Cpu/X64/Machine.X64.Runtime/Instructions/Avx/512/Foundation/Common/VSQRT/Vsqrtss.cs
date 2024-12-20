using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vsqrtss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vsqrtss_xmm_k1z_xmm_xmmm32_er:
                {
                    Vector128<float> dest = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    float parameter2 = ReadXmmScalarOrSingle(instruction, 1);
                    dest = dest.WithElement(0, MathF.Sqrt(parameter2));
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), dest);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
