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

            case Code.Jcxz_rel8_32:
                {
                    if (this.ProcessorRegisters.Cx == 0)
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

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
