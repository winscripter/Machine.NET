using Iced.Intel;
using Machine.Utility;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void wrmsr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Wrmsr:
                {
                    uint edx = this.ProcessorRegisters.Edx;
                    uint eax = this.ProcessorRegisters.Eax;
                    this.ProcessorRegisters.Msr = BitUtilities.CreateUInt64(edx, eax);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
