using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pcmpeqq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pcmpeqq_xmm_xmmm128:
                {
                    Vector128<ulong> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();

                    Vector128<ulong> result = Vector128.Create(
                        src[0] == dst[0] ? ulong.MaxValue : 0uL,
                        src[1] == dst[1] ? ulong.MaxValue : 0uL);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
