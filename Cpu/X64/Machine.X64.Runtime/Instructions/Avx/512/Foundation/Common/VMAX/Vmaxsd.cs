using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmaxsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmaxsd_xmm_k1z_xmm_xmmm64_sae:
                {
                    double scalar2 = ReadXmmScalarOrDouble(in instruction, 1);
                    Vector128<double> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    double scalar1 = parameter1.ToScalar();

                    double scalarResult = scalar1 > scalar2 ? scalar1 : scalar2;
                    parameter1 = parameter1.WithElement(0, scalarResult);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter1.As<double, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
