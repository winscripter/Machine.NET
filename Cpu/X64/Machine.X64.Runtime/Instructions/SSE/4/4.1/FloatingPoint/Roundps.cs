using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void roundps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Roundps_xmm_xmmm128_imm8:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        Round.VectorsSingle128(
                            EvaluateXmmFromInstruction(in instruction, 1),
                            (byte)instruction.GetImmediate(2)
                        )
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
