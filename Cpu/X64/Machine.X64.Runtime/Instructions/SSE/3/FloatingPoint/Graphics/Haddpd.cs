using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

// Difference between ADDPD and HADDPD:
// ADDPD:
//   Src: [a, b]
//   Dst: [e, f]
//   Res: [a+e, b+f]
// HADDPD:
//   Src: [a, b]
//   Dst: [e, f]
//   Res: [a+b, e+f]
// For better understanding, see Haddps.cs.

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void haddpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Haddpd_xmm_xmmm128:
                {
                    Vector128<double> dst = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> src = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();

                    Vector128<double> result = Vector128.Create(
                        src[0] + src[1],
                        dst[0] + dst[1]
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
