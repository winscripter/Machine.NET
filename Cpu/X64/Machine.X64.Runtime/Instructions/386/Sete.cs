using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void sete(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Sete_rm8:
                {
                    RMSet8(in instruction, this.ProcessorRegisters.RFlagsZF ? (byte)1 : (byte)0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
