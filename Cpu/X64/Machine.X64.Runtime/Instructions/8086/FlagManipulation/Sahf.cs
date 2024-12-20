using Iced.Intel;
using Machine.Utility;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void sahf(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Sahf:
                {
                    ushort flags = this.ProcessorRegisters.Flags;
                    BitUtilities.SetLower8Bits(ref flags, this.ProcessorRegisters.Ah);
                    this.ProcessorRegisters.Flags = flags;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
