using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void leave(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Leavew:
                {
                    this.ProcessorRegisters.Sp = this.ProcessorRegisters.Bp;
                    this.ProcessorRegisters.Bp = StackPopU16();
                    break;
                }

            case Code.Leaved:
                {
                    this.ProcessorRegisters.Esp = this.ProcessorRegisters.Ebp;
                    this.ProcessorRegisters.Ebp = StackPopU32();
                    break;
                }

            case Code.Leaveq:
                {
                    this.ProcessorRegisters.Rsp = this.ProcessorRegisters.Rbp;
                    this.ProcessorRegisters.Rbp = StackPopU64();
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
