using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kandn(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kandnb_kr_kr_kr:
                NewOpmaskCore.KAndnInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kandnw_kr_kr_kr:
                NewOpmaskCore.KAndnInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kandnd_kr_kr_kr:
                NewOpmaskCore.KAndnInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kandnq_kr_kr_kr:
                NewOpmaskCore.KAndnInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kandnb(in Instruction instruction) => kadd(in instruction);

    private void kandnw(in Instruction instruction) => kadd(in instruction);

    private void kandnd(in Instruction instruction) => kadd(in instruction);

    private void kandnq(in Instruction instruction) => kadd(in instruction);
}
