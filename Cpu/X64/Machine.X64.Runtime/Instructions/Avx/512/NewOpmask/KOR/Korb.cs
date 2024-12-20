using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void korb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Korb_kr_kr_kr:
                NewOpmaskCore.KorInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 8);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
