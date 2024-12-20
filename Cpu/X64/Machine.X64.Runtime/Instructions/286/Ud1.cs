using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ud1(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Ud1_r16_rm16:
            case Code.Ud1_r32_rm32:
            case Code.Ud1_r64_rm64:
                this.RaiseUndefinedOpCode();
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
