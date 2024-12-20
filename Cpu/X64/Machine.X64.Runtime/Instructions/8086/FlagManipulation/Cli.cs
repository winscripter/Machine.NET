using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cli(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cli:
                ProcessorRegisters.RFlagsIF = false;
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
