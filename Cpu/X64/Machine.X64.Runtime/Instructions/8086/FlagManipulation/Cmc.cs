using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmc(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmc:
                ProcessorRegisters.RFlagsCF = !ProcessorRegisters.RFlagsCF;
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
