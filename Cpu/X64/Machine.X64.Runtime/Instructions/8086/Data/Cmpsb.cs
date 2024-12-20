using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmpsb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmpsb_m8_m8:
                {
                    while (
                        instruction.HasRepPrefix
                        ? this.ProcessorRegisters.RFlagsZF
                        : instruction.HasRepnePrefix // REPNZ
                          ? !this.ProcessorRegisters.RFlagsZF
                          : false)
                    {

                        int src = this.Memory[(ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.Di];
                        int dst = this.Memory[(ulong)(this.ProcessorRegisters.Ds << 4) + this.ProcessorRegisters.Si];

                        int result = dst - src;
                        ProcessFlags((ushort)result);

                        if (this.ProcessorRegisters.RFlagsDF)
                        {
                            this.ProcessorRegisters.Si--;
                            this.ProcessorRegisters.Di--;
                        }
                        else
                        {
                            this.ProcessorRegisters.Si++;
                            this.ProcessorRegisters.Di++;
                        }
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
