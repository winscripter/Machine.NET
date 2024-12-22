using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void je(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Je_rel16:
                {
                    if (this.ProcessorRegisters.RFlagsZF == true)
                    {
                        short displacement = (short)instruction.NearBranch16;
                        if (displacement < 0)
                        {
                            this.ProcessorRegisters.Ip -= (ushort)Math.Abs(displacement);
                        }
                        else
                        {
                            this.ProcessorRegisters.Ip += (ushort)displacement;
                        }
                        break;
                    }
                    break;
                }

            case Code.Je_rel32_32:
                {
                    if (this.ProcessorRegisters.RFlagsZF == true)
                    {
                        int displacement = (int)instruction.NearBranch32;
                        if (displacement < 0)
                        {
                            this.ProcessorRegisters.Eip -= (uint)Math.Abs(displacement);
                        }
                        else
                        {
                            this.ProcessorRegisters.Eip += (uint)displacement;
                        }
                        break;
                    }
                    break;
                }

            case Code.Je_rel32_64:
                {
                    if (this.ProcessorRegisters.RFlagsZF == true)
                    {
                        long displacement = (long)instruction.NearBranch64;
                        if (displacement < 0L)
                        {
                            this.ProcessorRegisters.Rip -= (ulong)Math.Abs(displacement);
                        }
                        else
                        {
                            this.ProcessorRegisters.Rip += (ulong)displacement;
                        }
                        break;
                    }
                    break;
                }

            case Code.Je_rel8_16:
                {
                    if (this.ProcessorRegisters.RFlagsZF == true)
                    {
                        sbyte displacement = (sbyte)instruction.GetImmediate(0);
                        if (displacement < 0)
                        {
                            this.ProcessorRegisters.Ip -= (ushort)Math.Abs(displacement);
                        }
                        else
                        {
                            this.ProcessorRegisters.Ip += (ushort)displacement;
                        }
                        break;
                    }
                    break;
                }

            case Code.Je_rel8_32:
                {
                    if (this.ProcessorRegisters.RFlagsZF == true)
                    {
                        sbyte displacement = (sbyte)instruction.GetImmediate(0);
                        if (displacement < 0)
                        {
                            this.ProcessorRegisters.Eip -= (uint)Math.Abs(displacement);
                        }
                        else
                        {
                            this.ProcessorRegisters.Eip += (uint)displacement;
                        }
                        break;
                    }
                    break;
                }

            case Code.Je_rel8_64:
                {
                    if (this.ProcessorRegisters.RFlagsZF == true)
                    {
                        sbyte displacement = (sbyte)instruction.GetImmediate(0);
                        if (displacement < 0)
                        {
                            this.ProcessorRegisters.Rip -= (ulong)Math.Abs(displacement);
                        }
                        else
                        {
                            this.ProcessorRegisters.Rip += (ulong)displacement;
                        }
                        break;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
