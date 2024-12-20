using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kshiftr(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kshiftrb_kr_kr_imm8:
                NewOpmaskCore.KRightShiftInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kshiftrw_kr_kr_imm8:
                NewOpmaskCore.KRightShiftInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kshiftrd_kr_kr_imm8:
                NewOpmaskCore.KRightShiftInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kshiftrq_kr_kr_imm8:
                NewOpmaskCore.KRightShiftInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kshiftrb(in Instruction instruction) => kshiftr(in instruction);

    private void kshiftrw(in Instruction instruction) => kshiftr(in instruction);

    private void kshiftrd(in Instruction instruction) => kshiftr(in instruction);

    private void kshiftrq(in Instruction instruction) => kshiftr(in instruction);
}
