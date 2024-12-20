using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void clac(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Clac:
                {
                    this.ProcessorRegisters.RFlagsAC = false;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
