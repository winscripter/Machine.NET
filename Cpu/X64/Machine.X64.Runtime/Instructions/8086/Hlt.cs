using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void hlt(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Hlt:
                {
                    this.Busy = false;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
