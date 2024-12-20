using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void outsb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Outsb_DX_m8:
                {
                    this.ProcessorRegisters.Al = this.Memory[(ulong)(this.ProcessorRegisters.Ds << 4) + this.ProcessorRegisters.Si];
                    WriteIOPort(this.ProcessorRegisters.Dx, this.ProcessorRegisters.Al);
                    this.ProcessorRegisters.Si++;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
