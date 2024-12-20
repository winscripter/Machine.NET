using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cdq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cdq:
                {
                    if ((ProcessorRegisters.Eax & 0x8000) == 0x8000)
                    {
                        ProcessorRegisters.Eax = 0xFF;
                    }
                    else
                    {
                        ProcessorRegisters.Eax = 0x00;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
