using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void sti(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Sti:
                {
                    this.ProcessorRegisters.RFlagsIF = true;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
