using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmpsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmpsw_m16_m16:
                {
                    while (
                        instruction.HasRepPrefix
                        ? this.ProcessorRegisters.RFlagsZF
                        : instruction.HasRepnePrefix // REPNZ
                          ? !this.ProcessorRegisters.RFlagsZF
                          : false)
                    {

                        int src = this.Memory.ReadUInt16((ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.Di);
                        int dst = this.Memory.ReadUInt16((ulong)(this.ProcessorRegisters.Ds << 4) + this.ProcessorRegisters.Si);

                        int result = dst - src;
                        ProcessFlags((uint)result);

                        if (this.ProcessorRegisters.RFlagsDF)
                        {
                            this.ProcessorRegisters.Si -= 2;
                            this.ProcessorRegisters.Di -= 2;
                        }
                        else
                        {
                            this.ProcessorRegisters.Si += 2;
                            this.ProcessorRegisters.Di += 2;
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
