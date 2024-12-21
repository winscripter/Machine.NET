using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void fnclex(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Fnclex:
                {
                    this.Fpu.MaskedExceptions = ExceptionMask.Clear;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
