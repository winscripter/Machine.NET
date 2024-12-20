using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void insd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Insd_m32_DX:
                {
                    this.ProcessorRegisters.Al = (byte)ReadIOPort(this.ProcessorRegisters.Dx);
                    this.Memory[(ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.Rdi] = this.ProcessorRegisters.Al;
                    this.ProcessorRegisters.Rdi += 4;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
