using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kandb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kandb_kr_kr_kr:
                NewOpmaskCore.KAndInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 8);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
