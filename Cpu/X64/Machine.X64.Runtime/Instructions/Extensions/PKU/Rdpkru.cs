using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdpkru(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rdpkru:
                {
                    ProcessorRegisters.Eax = (uint)ProcessorRegisters.Pkru;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
