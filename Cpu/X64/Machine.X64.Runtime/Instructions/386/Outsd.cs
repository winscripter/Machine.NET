using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void outsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Outsd_DX_m32:
                {
                    this.ProcessorRegisters.Al = this.Memory[(ulong)(this.ProcessorRegisters.Ds << 4) + this.ProcessorRegisters.Si];
                    WriteIOPort(this.ProcessorRegisters.Dx, this.ProcessorRegisters.Al);
                    this.ProcessorRegisters.Rsi += 4;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
