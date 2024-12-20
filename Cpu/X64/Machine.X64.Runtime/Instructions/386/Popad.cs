using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void popad(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Popad:
                {
                    this.ProcessorRegisters.Edi = StackPopU32();
                    this.ProcessorRegisters.Esi = StackPopU32();
                    this.ProcessorRegisters.Ebp = StackPopU32();
                    this.ProcessorRegisters.Eax = StackPopU32();
                    this.ProcessorRegisters.Ebx = StackPopU32();
                    this.ProcessorRegisters.Edx = StackPopU32();
                    this.ProcessorRegisters.Ecx = StackPopU32();
                    this.ProcessorRegisters.Eax = StackPopU32();

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
