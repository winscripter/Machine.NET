using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void xadd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Xadd_rm16_r16:
                {
                    this.ProcessorRegisters.Cx = this.Memory.ReadUInt16(GetMemOperand16(instruction));
                    this.Memory.WriteUInt16(
                        GetMemOperand16(instruction),
                        (ushort)(this.Memory.ReadUInt16(GetMemOperand16(instruction)) + this.ProcessorRegisters.Ax)
                    );
                    this.ProcessorRegisters.Ax = this.ProcessorRegisters.Cx;
                    break;
                }

            case Code.Xadd_rm32_r32:
                {
                    this.ProcessorRegisters.Ecx = this.Memory.ReadUInt32(GetMemOperand16(instruction));
                    this.Memory.WriteUInt32(
                        GetMemOperand32(instruction),
                        (uint)(this.Memory.ReadUInt32(GetMemOperand32(instruction)) + this.ProcessorRegisters.Eax)
                    );
                    this.ProcessorRegisters.Eax = this.ProcessorRegisters.Ecx;
                    break;
                }

            case Code.Xadd_rm64_r64:
                {
                    this.ProcessorRegisters.Rcx = this.Memory.ReadUInt64(GetMemOperand64(instruction));
                    this.Memory.WriteUInt64(
                        GetMemOperand64(instruction),
                        (ulong)(this.Memory.ReadUInt64(GetMemOperand64(instruction)) + this.ProcessorRegisters.Rax)
                    );
                    this.ProcessorRegisters.Rax = this.ProcessorRegisters.Ecx;
                    break;
                }

            case Code.Xadd_rm8_r8:
                {
                    this.ProcessorRegisters.Cl = this.Memory[GetMemOperand16(instruction)];
                    this.Memory[GetMemOperand16(instruction)] = (byte)(this.Memory[GetMemOperand8(instruction)] + this.ProcessorRegisters.Al);
                    this.ProcessorRegisters.Al = this.ProcessorRegisters.Cl;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
