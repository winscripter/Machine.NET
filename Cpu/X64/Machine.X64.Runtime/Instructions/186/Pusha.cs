using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pusha(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pushaw:
                {
                    StackPush(ProcessorRegisters.Ax);
                    StackPush(ProcessorRegisters.Cx);
                    StackPush(ProcessorRegisters.Dx);
                    StackPush(ProcessorRegisters.Bx);
                    StackPush(ProcessorRegisters.Sp);
                    StackPush(ProcessorRegisters.Bp);
                    StackPush(ProcessorRegisters.Si);
                    StackPush(ProcessorRegisters.Di);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
