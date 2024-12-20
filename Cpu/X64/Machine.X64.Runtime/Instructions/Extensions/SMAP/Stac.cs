using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void stac(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Stac:
                {
                    this.ProcessorRegisters.RFlagsAC = true;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
