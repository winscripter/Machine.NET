using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

// Difference between ADDPS and HADDPS:
// ADDPS:
//   Src: [a, b, c, d]
//   Dst: [e, f, g, h]
//   Res: [a+e, b+f, c+g, d+h]
// HADDPS:
//   Src: [a, b, c, d]
//   Dst: [e, f, g, h]
//   Res: [a+b, c+d, e+f, g+h]

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void haddps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Haddps_xmm_xmmm128:
                {
                    Vector128<float> dst = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> src = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));

                    Vector128<float> result = Vector128.Create(
                        src[0] + src[1],
                        src[2] + src[3],
                        dst[0] + dst[1],
                        dst[2] + dst[3]
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
