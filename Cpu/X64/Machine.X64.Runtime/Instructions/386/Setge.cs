using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void setge(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Setge_rm8:
                {
                    RMSet8(in instruction, this.ProcessorRegisters.RFlagsSF == this.ProcessorRegisters.RFlagsOF ? (byte)1 : (byte)0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
