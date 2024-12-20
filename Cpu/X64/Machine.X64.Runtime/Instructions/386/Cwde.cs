using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cwde(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cwde:
                {
                    if ((ProcessorRegisters.Eax & 0x80000000) == 0x80000000)
                    {
                        ProcessorRegisters.Edx = 0xFFFFFFFF;
                    }
                    else
                    {
                        ProcessorRegisters.Edx = 0;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
