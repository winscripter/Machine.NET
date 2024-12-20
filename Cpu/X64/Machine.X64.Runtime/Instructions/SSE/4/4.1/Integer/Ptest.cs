using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ptest(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Ptest_xmm_xmmm128:
                {
                    Vector128<ulong> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    Vector128<ulong> result = src & dst;

                    bool zeroFlag = result[0] == 0u && result[1] == 0u;
                    this.ProcessorRegisters.RFlagsZF = zeroFlag;
                    this.ProcessorRegisters.RFlagsCF = !zeroFlag;

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
