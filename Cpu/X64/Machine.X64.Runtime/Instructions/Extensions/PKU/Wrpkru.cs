using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void wrpkru(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Wrpkru:
                {
                    ProcessorRegisters.Pkru = ProcessorRegisters.Eax;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
