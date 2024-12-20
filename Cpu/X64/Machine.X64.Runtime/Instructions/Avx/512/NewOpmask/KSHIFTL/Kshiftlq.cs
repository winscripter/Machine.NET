using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kshiftlq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kshiftlq_kr_kr_imm8:
                NewOpmaskCore.KLeftShiftInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
