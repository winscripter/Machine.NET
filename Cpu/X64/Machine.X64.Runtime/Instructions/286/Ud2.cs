using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ud2(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Ud2:
                this.RaiseUndefinedOpCode();
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
