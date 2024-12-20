using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void insw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Insw_m16_DX:
                {
                    this.ProcessorRegisters.Al = (byte)ReadIOPort(this.ProcessorRegisters.Dx);
                    this.Memory[(ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.Di] = this.ProcessorRegisters.Al;
                    this.ProcessorRegisters.Di += 2;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
