using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pop(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pop_r16:
                this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), StackPopU16());
                break;

            case Code.Pop_r32:
                this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), StackPopU32());
                break;

            case Code.Pop_r64:
                this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), StackPopU64());
                break;

            case Code.Pop_rm16:
                RMSet16(in instruction, StackPopU16());
                break;

            case Code.Pop_rm32:
                RMSet32(in instruction, StackPopU32());
                break;

            case Code.Pop_rm64:
                RMSet64(in instruction, StackPopU64());
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
