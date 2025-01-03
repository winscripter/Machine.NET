﻿using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void jnp(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Jnp_rel16:
                {
                    if (!this.ProcessorRegisters.RFlagsPF)
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
                    else
                    {
                        this.ProcessorRegisters.Ip += (ushort)instruction.Length;
                    }
                    break;
                }

            case Code.Jnp_rel32_32:
                {
                    if (!this.ProcessorRegisters.RFlagsPF)
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
                    else
                    {
                        this.ProcessorRegisters.Eip += (ushort)instruction.Length;
                    }
                    break;
                }

            case Code.Jnp_rel32_64:
                {
                    if (!this.ProcessorRegisters.RFlagsPF)
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
                    else
                    {
                        this.ProcessorRegisters.Rip += (ushort)instruction.Length;
                    }
                    break;
                }

            case Code.Jnp_rel8_16:
                {
                    if (!this.ProcessorRegisters.RFlagsPF)
                    {
                        sbyte displacement = (sbyte)instruction.NearBranch16;
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
                    else
                    {
                        this.ProcessorRegisters.Ip += (ushort)instruction.Length;
                    }
                    break;
                }

            case Code.Jnp_rel8_32:
                {
                    if (!this.ProcessorRegisters.RFlagsPF)
                    {
                        sbyte displacement = (sbyte)instruction.NearBranch16;
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
                    else
                    {
                        this.ProcessorRegisters.Eip += (ushort)instruction.Length;
                    }
                    break;
                }

            case Code.Jnp_rel8_64:
                {
                    if (!this.ProcessorRegisters.RFlagsPF)
                    {
                        sbyte displacement = (sbyte)instruction.NearBranch16;
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
                    else
                    {
                        this.ProcessorRegisters.Rip += (ushort)instruction.Length;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
