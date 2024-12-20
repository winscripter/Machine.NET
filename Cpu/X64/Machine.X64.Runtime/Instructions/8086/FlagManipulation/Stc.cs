using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void stc(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Stc:
                {
                    this.ProcessorRegisters.RFlagsCF = true;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
