using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void not(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Not_rm16:
                {
                    RMSet16(in instruction, (ushort)~RMEvaluate16(in instruction, 0), 0);
                    break;
                }

            case Code.Not_rm32:
                {
                    RMSet32(in instruction, ~RMEvaluate32(in instruction, 0), 0);
                    break;
                }

            case Code.Not_rm64:
                {
                    RMSet64(in instruction, ~RMEvaluate64(in instruction, 0), 0);
                    break;
                }

            case Code.Not_rm8:
                {
                    RMSet8(in instruction, (byte)~RMEvaluate8(in instruction, 0), 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
