using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pushfd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pushfd:
                {
                    this.StackPush(this.ProcessorRegisters.EFlags);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
