using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movdq2q(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movdq2q_mm_xmm:
                {
                    Vector128<ulong> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), vec[0]);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
