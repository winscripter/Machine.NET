using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void roundpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Roundpd_xmm_xmmm128_imm8:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        Round.VectorsDouble128(
                            EvaluateXmmFromInstruction(in instruction, 1).As<float, double>(),
                            (byte)instruction.GetImmediate(2)
                        ).As<double, float>()
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
