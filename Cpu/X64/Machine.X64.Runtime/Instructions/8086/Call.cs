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
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand16(instruction));
                    ushort offset = this.Memory.ReadUInt16(GetMemOperand16(instruction) + 2);
                    StackPush(this.ProcessorRegisters.Cs);
                    StackPush(this.ProcessorRegisters.Ip);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Ip = offset;
                    break;
                }

            case Code.Call_m1632:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand16(instruction));
                    uint offset = this.Memory.ReadUInt32(GetMemOperand16(instruction) + 2);
                    StackPush(this.ProcessorRegisters.Cs);
                    StackPush(this.ProcessorRegisters.Eip);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Eip = offset;
                    break;
                }

            case Code.Call_m1664:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand16(instruction));
                    ulong offset = this.Memory.ReadUInt64(GetMemOperand16(instruction) + 2);
                    StackPush(this.ProcessorRegisters.Cs);
                    StackPush(this.ProcessorRegisters.Rip);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Rip = offset;
                    break;
                }

            case Code.Call_rel16:
                {
                    ushort displacement = (ushort)instruction.GetImmediate(0);
                    StackPush(this.ProcessorRegisters.Ip);
                    this.ProcessorRegisters.Ip += displacement;
                    break;
                }

            case Code.Call_rel32_32:
                {
                    uint displacement = (uint)instruction.GetImmediate(0);
                    StackPush(this.ProcessorRegisters.Eip);
                    this.ProcessorRegisters.Eip += displacement;
                    break;
                }

            case Code.Call_rel32_64:
                {
                    uint displacement = (uint)instruction.GetImmediate(0);
                    StackPush(this.ProcessorRegisters.Rip);
                    this.ProcessorRegisters.Rip += displacement;
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
