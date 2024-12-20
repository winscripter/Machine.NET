using Iced.Intel;
using Machine.Utility;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdtsc(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rdtsc:
                {
                    ulong tsc = this.Tsc;
                    this.ProcessorRegisters.Edx = BitUtilities.GetUpper32Bits(tsc);
                    this.ProcessorRegisters.Eax = BitUtilities.GetLower32Bits(tsc);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
