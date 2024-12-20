using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void neg(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Neg_rm8:
                {
                    RMSet8(in instruction, (byte)(~RMEvaluate8(in instruction, 0) + 1), 0);
                    break;
                }

            case Code.Neg_rm16:
                {
                    RMSet16(in instruction, (ushort)(~RMEvaluate16(in instruction, 0) + 1), 0);
                    break;
                }

            case Code.Neg_rm32:
                {
                    RMSet32(in instruction, (uint)(~RMEvaluate32(in instruction, 0) + 1), 0);
                    break;
                }

            case Code.Neg_rm64:
                {
                    RMSet64(in instruction, (ulong)(~RMEvaluate64(in instruction, 0) + 1), 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
