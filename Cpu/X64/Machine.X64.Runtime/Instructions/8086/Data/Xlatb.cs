using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void xlatb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Xlat_m8:
                {
                    this.ProcessorRegisters.Al = this.Memory[(uint)this.ProcessorRegisters.Bx + this.ProcessorRegisters.Al];
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
