using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void outsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Outsw_DX_m16:
                {
                    this.ProcessorRegisters.Al = this.Memory[(ulong)(this.ProcessorRegisters.Ds << 4) + this.ProcessorRegisters.Si];
                    WriteIOPort(this.ProcessorRegisters.Dx, this.ProcessorRegisters.Al);
                    this.ProcessorRegisters.Si += 2;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
