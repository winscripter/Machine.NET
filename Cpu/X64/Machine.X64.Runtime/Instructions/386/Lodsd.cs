using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void lodsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Lodsd_EAX_m32:
                {
                    uint m32 = this.Memory.ReadUInt32(NormalizeAddress(this.ProcessorRegisters.Esi));
                    this.ProcessorRegisters.Eax = m32;
                    if (this.ProcessorRegisters.RFlagsDF)
                    {
                        this.ProcessorRegisters.Esi -= 4;
                    }
                    else
                    {
                        this.ProcessorRegisters.Esi += 4;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
