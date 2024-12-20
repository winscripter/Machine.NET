using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pushad(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pushad:
                {
                    StackPush(ProcessorRegisters.Eax);
                    StackPush(ProcessorRegisters.Ecx);
                    StackPush(ProcessorRegisters.Edx);
                    StackPush(ProcessorRegisters.Ebx);
                    StackPush(ProcessorRegisters.Esp);
                    StackPush(ProcessorRegisters.Ebp);
                    StackPush(ProcessorRegisters.Esi);
                    StackPush(ProcessorRegisters.Edi);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
