using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vminss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vminss_xmm_k1z_xmm_xmmm32_sae:
                {
                    float scalar2 = ReadXmmScalarOrSingle(in instruction, 1);
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    float scalar1 = parameter1.ToScalar();

                    float scalarResult = scalar1 < scalar2 ? scalar1 : scalar2;
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
