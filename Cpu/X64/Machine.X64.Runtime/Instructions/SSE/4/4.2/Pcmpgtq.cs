using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pcmpgtq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pcmpgtq_xmm_xmmm128:
                {
                    Vector128<long> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsInt64();
                    Vector128<long> src = EvaluateXmmFromInstruction(in instruction, 1).AsInt64();

                    dst = dst.WithElement(0, dst[0] > src[0] ? long.MaxValue : 0L);
                    dst = dst.WithElement(1, dst[1] > src[1] ? long.MaxValue : 0L);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), dst.AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
