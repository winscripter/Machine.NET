using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void das(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Das:
                {
                    byte oldAL = this.ProcessorRegisters.Al;
                    bool oldCF = this.ProcessorRegisters.RFlagsCF;

                    if ((this.ProcessorRegisters.Al & 0x0F) > 9 || this.ProcessorRegisters.RFlagsAF)
                    {
                        this.ProcessorRegisters.RFlagsCF = this.ProcessorRegisters.Al - 6 > byte.MaxValue;
                        this.ProcessorRegisters.RFlagsCF = oldCF | this.ProcessorRegisters.RFlagsCF;
                        this.ProcessorRegisters.RFlagsAF = true;

                        this.ProcessorRegisters.Al -= 6;
                    }
                    else
                    {
                        this.ProcessorRegisters.RFlagsAF = false;
                    }

                    if ((oldAL > 0x99) | oldCF)
                    {
                        this.ProcessorRegisters.Al -= 0x60;
                        this.ProcessorRegisters.RFlagsCF = true;
                    }
                    else
                    {
                        this.ProcessorRegisters.RFlagsCF = true;
                    }

                    this.ProcessorRegisters.RFlagsSF = this.ProcessorRegisters.Al % 2 == 0;
                    this.ProcessorRegisters.RFlagsZF = this.ProcessorRegisters.Al == 0;
                    this.ProcessorRegisters.RFlagsPF = GetParityOfLowerEightBits((ushort)this.ProcessorRegisters.Al);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
