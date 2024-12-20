using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kand(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kandb_kr_kr_kr:
                NewOpmaskCore.KAndInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kandw_kr_kr_kr:
                NewOpmaskCore.KAndInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kandd_kr_kr_kr:
                NewOpmaskCore.KAndInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kandq_kr_kr_kr:
                NewOpmaskCore.KAndInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kandb(in Instruction instruction) => kadd(in instruction);

    private void kandw(in Instruction instruction) => kadd(in instruction);

    private void kandd(in Instruction instruction) => kadd(in instruction);

    private void kandq(in Instruction instruction) => kadd(in instruction);
}
