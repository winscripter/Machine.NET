using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kxnorw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kxnorw_kr_kr_kr:
                NewOpmaskCore.KXnorInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 16);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
