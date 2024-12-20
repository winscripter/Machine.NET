using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kaddb(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (instruction.Code)
        {
            case Code.VEX_Kaddb_kr_kr_kr:
                NewOpmaskCore.KAddInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
