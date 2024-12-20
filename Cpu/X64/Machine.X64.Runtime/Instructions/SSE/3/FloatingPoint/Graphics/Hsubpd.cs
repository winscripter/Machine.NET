using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

// Difference between SUBPD and SUBPD:
// SUBPD:
//   Src: [a, b]
//   Dst: [e, f]
//   Res: [a+e, b+f]
// HSUBPD:
//   Src: [a, b]
//   Dst: [e, f]
//   Res: [a+b, e+f]
// For better understanding, see Hsubps.cs.

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void hsubpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Hsubpd_xmm_xmmm128:
                {
                    Vector128<double> dst = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> src = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();

                    Vector128<double> result = Vector128.Create(
                        src[0] - src[1],
                        dst[0] - dst[1]
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
