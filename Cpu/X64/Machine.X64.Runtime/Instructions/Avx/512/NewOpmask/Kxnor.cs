using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kxnor(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kxnorb_kr_kr_kr:
                NewOpmaskCore.KXnorInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kxnorw_kr_kr_kr:
                NewOpmaskCore.KAndnInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kxnord_kr_kr_kr:
                NewOpmaskCore.KXnorInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kxnorq_kr_kr_kr:
                NewOpmaskCore.KXnorInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kxnorb(in Instruction instruction) => kxnor(in instruction);

    private void kxnorw(in Instruction instruction) => kxnor(in instruction);

    private void kxnord(in Instruction instruction) => kxnor(in instruction);

    private void kxnorq(in Instruction instruction) => kxnor(in instruction);
}
