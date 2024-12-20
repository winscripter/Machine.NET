using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void lahf(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Lahf:
                {
                    this.ProcessorRegisters.Ah = (byte)(this.ProcessorRegisters.Flags & 0x00ff);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
