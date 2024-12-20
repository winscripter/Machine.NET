using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kaddq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kaddq_kr_kr_kr:
                NewOpmaskCore.KAddInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
