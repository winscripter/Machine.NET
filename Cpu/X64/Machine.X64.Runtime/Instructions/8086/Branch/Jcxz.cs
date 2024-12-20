using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void jcxz(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Jcxz_rel8_16:
                {
                    if (this.ProcessorRegisters.Cx == 0)
                    {
                        byte displacement = (byte)instruction.GetImmediate(0);
                        this.ProcessorRegisters.Ip += displacement;
                    }
                    break;
                }

            case Code.Jcxz_rel8_32:
                {
                    if (this.ProcessorRegisters.Cx == 0)
                    {
                        byte displacement = (byte)instruction.GetImmediate(0);
                        this.ProcessorRegisters.Eip += displacement;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
