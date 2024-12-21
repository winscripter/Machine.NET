using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void jmp(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Jmp_m1616:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                    ushort offset = this.Memory.ReadUInt16(GetMemOperand16(in instruction) + 2);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Ip = offset;
                    break;
                }

            case Code.Jmp_m1632:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                    uint offset = this.Memory.ReadUInt32(GetMemOperand16(in instruction) + 2);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Eip = offset;
                    break;
                }

            case Code.Jmp_m1664:
                {
                    ushort segmentSelector = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                    ulong offset = this.Memory.ReadUInt64(GetMemOperand16(in instruction) + 2);
                    this.ProcessorRegisters.Cs = segmentSelector;
                    this.ProcessorRegisters.Rip = offset;
                    break;
                }

            case Code.Jmp_rel16:
                {
                    ushort displacement = (ushort)instruction.GetImmediate(0);
                    this.ProcessorRegisters.Ip += displacement;
                    break;
                }

            case Code.Jmp_rel32_32:
                {
                    uint displacement = (uint)instruction.GetImmediate(0);
                    this.ProcessorRegisters.Eip += displacement;
                    break;
                }

            case Code.Jmp_rel32_64:
                {
                    uint displacement = (uint)instruction.GetImmediate(0);
                    this.ProcessorRegisters.Rip += displacement;
                    break;
                }

            case Code.Jmp_rm16:
                {
                    this.ProcessorRegisters.Ip = RMEvaluate16(instruction, 0);
                    break;
                }

            case Code.Jmp_rm32:
                {
                    this.ProcessorRegisters.Eip = RMEvaluate32(instruction, 0);
                    break;
                }

            case Code.Jmp_rm64:
                {
                    this.ProcessorRegisters.Rip = RMEvaluate64(instruction, 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
