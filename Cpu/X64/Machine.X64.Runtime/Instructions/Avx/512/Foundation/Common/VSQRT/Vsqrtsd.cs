using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vsqrtsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vsqrtsd_xmm_k1z_xmm_xmmm64_er:
                {
                    Vector128<double> dest = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsDouble();
                    double parameter2 = ReadXmmScalarOrDouble(instruction, 1);
                    dest = dest.WithElement(0, Math.Sqrt(parameter2));
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), dest.AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
