using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pushf(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pushfw:
                {
                    StackPush(this.ProcessorRegisters.Flags);
                    break;
                }

            case Code.Pushfd:
                {
                    StackPush(this.ProcessorRegisters.EFlags);
                    break;
                }

            case Code.Pushfq:
                {
                    StackPush(this.ProcessorRegisters.RFlags);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
