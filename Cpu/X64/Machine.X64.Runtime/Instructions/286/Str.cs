using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void str(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
