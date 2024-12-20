using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movshdup(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movshdup_xmm_xmmm128:
                {
                    Vector128<float> input = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = Vector128.Create(input[1], input[1], input[3], input[3]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
