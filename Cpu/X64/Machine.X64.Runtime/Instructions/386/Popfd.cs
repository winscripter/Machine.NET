using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void popfd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Popfd:
                {
                    this.ProcessorRegisters.EFlags = this.StackPopU32();
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
