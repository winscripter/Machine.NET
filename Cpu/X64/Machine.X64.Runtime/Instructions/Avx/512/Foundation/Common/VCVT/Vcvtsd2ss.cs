using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtsd2ss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtsd2ss_xmm_k1z_xmm_xmmm64_er:
                {
                    double scalar = ReadXmmScalarOrDouble(in instruction, 2);
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    xmm = xmm.WithElement(0, (float)scalar);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
