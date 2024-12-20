using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movhlps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movhlps_xmm_xmm:
                {
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> parameter2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));

                    // Example: MOVHLPS XMM1, XMM2
                    // The lower part of XMM1 becomes the upper part of XMM2
                    parameter1 = parameter1.WithLower(parameter2.GetUpper());

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter1);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
