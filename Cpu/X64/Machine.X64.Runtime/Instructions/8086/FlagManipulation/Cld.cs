using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cld(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cld:
                ProcessorRegisters.RFlagsDF = false;
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
