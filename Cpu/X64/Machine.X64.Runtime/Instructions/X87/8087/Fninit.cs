using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void fninit(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Fninit:
                this.Fpu.Reset();
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
