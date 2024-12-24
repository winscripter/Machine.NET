using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ret(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Retnd:
                {
                    this.ProcessorRegisters.Eip = StackPopU32();
                    break;
                }

            case Code.Retnd_imm16:
                {
                    this.ProcessorRegisters.Eip = StackPopU32();
                    this.ProcessorRegisters.Rsp += (ushort)instruction.GetImmediate(0);
                    break;
                }

            case Code.Retnq:
                {
                    this.ProcessorRegisters.Rip = StackPopU64();
                    break;
                }

            case Code.Retnq_imm16:
                {
                    this.ProcessorRegisters.Rip = StackPopU64();
                    this.ProcessorRegisters.Rsp += (ushort)instruction.GetImmediate(0);
                    break;
                }

            case Code.Retnw:
                {
                    this.ProcessorRegisters.Ip = StackPopU16();
                    break;
                }

            case Code.Retnw_imm16:
                {
                    this.ProcessorRegisters.Ip = StackPopU16();
                    this.ProcessorRegisters.Rsp += (ushort)instruction.GetImmediate(0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
