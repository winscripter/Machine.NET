using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtps2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtps2dq_xmm_xmmm128:
                {
                    Vector128<float> vec = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<int> newVec = Vector128.Create([(int)vec[0], (int)vec[1], (int)vec[2], (int)vec[3]]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), newVec.As<int, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
