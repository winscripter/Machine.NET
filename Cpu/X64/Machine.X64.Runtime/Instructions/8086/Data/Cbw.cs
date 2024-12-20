using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cbw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cbw:
                {
                    if ((ProcessorRegisters.Al & 0x80) == 0x80)
                    {
                        ProcessorRegisters.Ah = 0xFF;
                    }
                    else
                    {
                        ProcessorRegisters.Ah = 0x00;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
