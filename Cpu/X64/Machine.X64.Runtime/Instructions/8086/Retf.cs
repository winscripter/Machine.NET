using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void retf(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Retfd:
                {
                    this.ProcessorRegisters.Eip = StackPopU32();
                    this.ProcessorRegisters.Cs = StackPopU16();
                    break;
                }

            case Code.Retfd_imm16:
                {
                    this.ProcessorRegisters.Eip = StackPopU32();
                    this.ProcessorRegisters.Cs = StackPopU16();
                    this.ProcessorRegisters.Rsp += (ushort)instruction.GetImmediate(0);
                    break;
                }

            case Code.Retfq:
                {
                    this.ProcessorRegisters.Rip = StackPopU64();
                    this.ProcessorRegisters.Cs = StackPopU16();
                    break;
                }

            case Code.Retfq_imm16:
                {
                    this.ProcessorRegisters.Rip = StackPopU64();
                    this.ProcessorRegisters.Cs = StackPopU16();
                    this.ProcessorRegisters.Rsp += (ushort)instruction.GetImmediate(0);
                    break;
                }

            case Code.Retfw:
                {
                    this.ProcessorRegisters.Ip = StackPopU16();
                    this.ProcessorRegisters.Cs = StackPopU16();
                    break;
                }

            case Code.Retfw_imm16:
                {
                    this.ProcessorRegisters.Ip = StackPopU16();
                    this.ProcessorRegisters.Cs = StackPopU16();
                    this.ProcessorRegisters.Rsp += (ushort)instruction.GetImmediate(0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
