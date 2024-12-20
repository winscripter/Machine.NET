using Iced.Intel;
 
namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void nop(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Nopd:
            case Code.Nopw:
            case Code.Nopq:
            case Code.Nop_rm16:
            case Code.Nop_rm32:
            case Code.Nop_rm64:
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
