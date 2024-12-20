using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void setne(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Setne_rm8:
                {
                    RMSet8(in instruction, !this.ProcessorRegisters.RFlagsZF ? (byte)1 : (byte)0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
