using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kshiftl(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kshiftlb_kr_kr_imm8:
                NewOpmaskCore.KLeftShiftInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kshiftlw_kr_kr_imm8:
                NewOpmaskCore.KLeftShiftInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kshiftld_kr_kr_imm8:
                NewOpmaskCore.KLeftShiftInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kshiftlq_kr_kr_imm8:
                NewOpmaskCore.KLeftShiftInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kshiftlb(in Instruction instruction) => kadd(in instruction);

    private void kshiftlw(in Instruction instruction) => kadd(in instruction);

    private void kshiftld(in Instruction instruction) => kadd(in instruction);

    private void kshiftlq(in Instruction instruction) => kadd(in instruction);
}
