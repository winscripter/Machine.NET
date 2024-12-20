using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movsldup(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movsldup_xmm_xmmm128:
                {
                    Vector128<float> input = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = Vector128.Create(input[0], input[0], input[2], input[2]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
