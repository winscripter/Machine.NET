using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ud0(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Ud0:
            case Code.Ud0_r16_rm16:
            case Code.Ud0_r32_rm32:
            case Code.Ud0_r64_rm64:
                this.RaiseUndefinedOpCode();
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
