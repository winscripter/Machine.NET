using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void popf(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Popfw:
                {
                    this.ProcessorRegisters.Flags = StackPopU16();
                    break;
                }

            case Code.Popfd:
                {
                    this.ProcessorRegisters.EFlags = StackPopU32();
                    break;
                }

            case Code.Popfq:
                {
                    this.ProcessorRegisters.RFlags = StackPopU64();
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
