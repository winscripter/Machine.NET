using Iced.Intel;
using Machine.Utility;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdmsr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rdmsr:
                {
                    ulong msr = this.ProcessorRegisters.Msr;
                    uint edx = BitUtilities.GetUpper32Bits(msr);
                    uint eax = BitUtilities.GetLower32Bits(msr);
                    this.ProcessorRegisters.Edx = edx;
                    this.ProcessorRegisters.Eax = eax;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
