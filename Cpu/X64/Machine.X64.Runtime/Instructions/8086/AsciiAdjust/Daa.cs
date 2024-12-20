using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void daa(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Daa:
                {
                    byte previousAL = ProcessorRegisters.Al;
                    bool previousCF = ProcessorRegisters.RFlagsCF;

                    if ((ProcessorRegisters.Al & 0x0F) > 9 || ProcessorRegisters.RFlagsAF)
                    {
                        ProcessorRegisters.RFlagsCF = (ushort)(ProcessorRegisters.Al + 6) > byte.MaxValue;
                        ProcessorRegisters.RFlagsCF |= previousCF;
                        ProcessorRegisters.RFlagsAF = true;

                        ProcessorRegisters.Al += 6;
                    }
                    else
                    {
                        ProcessorRegisters.RFlagsAF = false;
                    }

                    if ((previousAL > 0x99) | previousCF)
                    {
                        ProcessorRegisters.Al += 0x60;
                        ProcessorRegisters.RFlagsCF = true;
                    }
                    else
                    {
                        ProcessorRegisters.RFlagsCF = false;
                    }

                    ProcessorRegisters.RFlagsSF = ProcessorRegisters.Al % 2 == 0;
                    ProcessorRegisters.RFlagsZF = ProcessorRegisters.Al == 0;
                    ProcessorRegisters.RFlagsPF = GetParityOfLowerEightBits(ProcessorRegisters.Al);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
