using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void stosd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Stosd_m32_EAX:
                {
                    this.Memory.WriteUInt32(NormalizeAddress(this.ProcessorRegisters.Edi), this.ProcessorRegisters.Eax);
                    if (this.ProcessorRegisters.RFlagsDF)
                    {
                        this.ProcessorRegisters.Edi -= 4;
                    }
                    else
                    {
                        this.ProcessorRegisters.Edi += 4;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
