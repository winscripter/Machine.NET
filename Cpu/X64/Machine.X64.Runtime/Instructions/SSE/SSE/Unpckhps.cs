using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void unpckhps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Unpckhps_xmm_xmmm128:
                {
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> parameter2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));

                    // Example: UNPCKLPS XMM1, XMM2
                    // XMM1 = [A, B, C, D]
                    // XMM2 = [E, F, G, H]
                    // After running instruction:
                    // XMM1 = [C, G, D, H]

                    parameter1 = parameter1.WithElement(0, parameter1[2]);
                    parameter1 = parameter1.WithElement(1, parameter2[2]);
                    parameter1 = parameter1.WithElement(2, parameter1[3]);
                    parameter1 = parameter1.WithElement(3, parameter2[3]);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter1);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
