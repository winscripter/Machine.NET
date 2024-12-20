using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void std(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Std:
                {
                    this.ProcessorRegisters.RFlagsDF = true;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
