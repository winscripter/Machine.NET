using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void clc(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Clc:
                ProcessorRegisters.RFlagsCF = false;
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
