using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void jecxz(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Jecxz_rel8_16:
                {
                    if (this.ProcessorRegisters.Ecx == 0)
                    {
                        ushort relativeOffset = (ushort)instruction.GetImmediate(0);

                        ulong actualAddress = relativeOffset < 0
                            ? this.ProcessorRegisters.Rip + 1 + relativeOffset
                            : this.ProcessorRegisters.Rip + 1 - relativeOffset;

                        this.ProcessorRegisters.Rip = actualAddress;
                    }
                    break;
                }

            case Code.Jecxz_rel8_32:
                {
                    if (this.ProcessorRegisters.Ecx == 0)
                    {
                        uint relativeOffset = (uint)instruction.GetImmediate(0);

                        ulong actualAddress = relativeOffset < 0
                            ? this.ProcessorRegisters.Rip + 1 + relativeOffset
                            : this.ProcessorRegisters.Rip + 1 - relativeOffset;

                        this.ProcessorRegisters.Rip = actualAddress;
                    }
                    break;
                }

            case Code.Jecxz_rel8_64:
                {
                    if (this.ProcessorRegisters.Ecx == 0)
                    {
                        ulong relativeOffset = (ulong)instruction.GetImmediate(0);

                        ulong actualAddress = relativeOffset < 0
                            ? this.ProcessorRegisters.Rip + 1 + relativeOffset
                            : this.ProcessorRegisters.Rip + 1 - relativeOffset;

                        this.ProcessorRegisters.Rip = actualAddress;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
