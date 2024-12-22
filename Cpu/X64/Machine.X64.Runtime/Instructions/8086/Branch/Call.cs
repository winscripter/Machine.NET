using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void call(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Call_m1616:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand(in instruction));
                    ushort offset = this.Memory.ReadUInt16(GetMemOperand(in instruction) + 2);
                    StackPush(this.ProcessorRegisters.Cs);
                    StackPush(this.ProcessorRegisters.Ip);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Ip = offset;
                    break;
                }

            case Code.Call_m1632:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand(in instruction));
                    uint offset = this.Memory.ReadUInt32(GetMemOperand(in instruction) + 2);
                    StackPush(this.ProcessorRegisters.Cs);
                    StackPush(this.ProcessorRegisters.Eip);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Eip = offset;
                    break;
                }

            case Code.Call_m1664:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand(in instruction));
                    ulong offset = this.Memory.ReadUInt64(GetMemOperand(in instruction) + 2);
                    StackPush(this.ProcessorRegisters.Cs);
                    StackPush(this.ProcessorRegisters.Rip);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Rip = offset;
                    break;
                }

            case Code.Call_rel16:
                {
                    short displacement = (short)instruction.NearBranch16;
                    StackPush(this.ProcessorRegisters.Ip);
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

            case Code.Call_rel32_32:
                {
                    int displacement = (int)instruction.NearBranch32;
                    StackPush(this.ProcessorRegisters.Eip);
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

            case Code.Call_rel32_64:
                {
                    long displacement = (long)instruction.NearBranch64;
                    StackPush(this.ProcessorRegisters.Rip);
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

            case Code.Call_rm16:
                {
                    StackPush(this.ProcessorRegisters.Ip);
                    this.ProcessorRegisters.Ip = RMEvaluate16(instruction, 0);
                    break;
                }

            case Code.Call_rm32:
                {
                    StackPush(this.ProcessorRegisters.Eip);
                    this.ProcessorRegisters.Eip = RMEvaluate32(instruction, 0);
                    break;
                }

            case Code.Call_rm64:
                {
                    StackPush(this.ProcessorRegisters.Rip);
                    this.ProcessorRegisters.Rip = RMEvaluate64(instruction, 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
