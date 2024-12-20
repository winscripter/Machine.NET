using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kxord(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kxord_kr_kr_kr:
                NewOpmaskCore.KXnorInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 32);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
