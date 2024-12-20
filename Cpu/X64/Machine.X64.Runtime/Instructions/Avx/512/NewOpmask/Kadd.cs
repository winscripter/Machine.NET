using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kadd(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kaddb_kr_kr_kr:
                NewOpmaskCore.KAddInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kaddw_kr_kr_kr:
                NewOpmaskCore.KAddInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kaddd_kr_kr_kr:
                NewOpmaskCore.KAddInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kaddq_kr_kr_kr:
                NewOpmaskCore.KAddInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kaddb(in Instruction instruction) => kadd(in instruction);

    private void kaddw(in Instruction instruction) => kadd(in instruction);

    private void kaddd(in Instruction instruction) => kadd(in instruction);

    private void kaddq(in Instruction instruction) => kadd(in instruction);
}
