using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void setns(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Setns_rm8:
                {
                    RMSet8(in instruction, !this.ProcessorRegisters.RFlagsSF ? (byte)1 : (byte)0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
