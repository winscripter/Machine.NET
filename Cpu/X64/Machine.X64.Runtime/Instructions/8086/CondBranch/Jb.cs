using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void jb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Jb_rel16:
                {
                    ushort displacement = (ushort)instruction.GetImmediate(0);
                    if (this.ProcessorRegisters.RFlagsCF == true)
                    {
                        this.ProcessorRegisters.Ip += displacement;
                    }
                    break;
                }

            case Code.Jb_rel32_32:
                {
                    uint displacement = (uint)instruction.GetImmediate(0);
                    if (this.ProcessorRegisters.RFlagsCF == true)
                    {
                        this.ProcessorRegisters.Eip += displacement;
                    }
                    break;
                }

            case Code.Jb_rel32_64:
                {
                    uint displacement = (uint)instruction.GetImmediate(0);
                    if (this.ProcessorRegisters.RFlagsCF == true)
                    {
                        this.ProcessorRegisters.Rip += displacement;
                    }
                    break;
                }

            case Code.Jb_rel8_16:
                {
                    byte displacement = (byte)instruction.GetImmediate(0);
                    if (this.ProcessorRegisters.RFlagsCF == true)
                    {
                        this.ProcessorRegisters.Ip += (ushort)displacement;
                    }
                    break;
                }

            case Code.Jb_rel8_32:
                {
                    byte displacement = (byte)instruction.GetImmediate(0);
                    if (this.ProcessorRegisters.RFlagsCF == true)
                    {
                        this.ProcessorRegisters.Eip += (uint)displacement;
                    }
                    break;
                }

            case Code.Jb_rel8_64:
                {
                    byte displacement = (byte)instruction.GetImmediate(0);
                    if (this.ProcessorRegisters.RFlagsCF == true)
                    {
                        this.ProcessorRegisters.Rip += (ulong)displacement;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
