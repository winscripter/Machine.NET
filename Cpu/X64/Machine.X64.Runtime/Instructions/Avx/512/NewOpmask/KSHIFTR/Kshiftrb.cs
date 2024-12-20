using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kshiftrb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kshiftrb_kr_kr_imm8:
                NewOpmaskCore.KRightShiftInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 8);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
