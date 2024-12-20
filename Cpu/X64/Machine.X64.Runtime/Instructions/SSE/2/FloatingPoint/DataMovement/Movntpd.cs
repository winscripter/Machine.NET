using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movntpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movntpd_m128_xmm:
                {
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    this.Memory.WriteBinaryVector128(GetMemOperand64(instruction), xmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
