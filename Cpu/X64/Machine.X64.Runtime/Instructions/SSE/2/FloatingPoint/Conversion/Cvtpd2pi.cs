using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtpd2pi(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtpd2pi_mm_xmmm128:
                {
                    Vector128<uint> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
