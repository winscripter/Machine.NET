using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void addsubps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Addsubps_xmm_xmmm128:
                {
                    Vector128<float> src = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));

                    Vector128<float> result = Vector128.Create(
                        src[0] - dst[0],
                        src[1] + dst[1],
                        src[2] - dst[2],
                        src[3] + dst[3]
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
