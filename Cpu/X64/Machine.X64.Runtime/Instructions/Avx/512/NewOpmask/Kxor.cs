using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kxor(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Kxorb_kr_kr_kr:
                NewOpmaskCore.KXorInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kxorw_kr_kr_kr:
                NewOpmaskCore.KXorInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kxord_kr_kr_kr:
                NewOpmaskCore.KXorInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kxorq_kr_kr_kr:
                NewOpmaskCore.KXorInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void kxorb(in Instruction instruction) => kxnor(in instruction);

    private void kxorw(in Instruction instruction) => kxnor(in instruction);

    private void kxord(in Instruction instruction) => kxnor(in instruction);

    private void kxorq(in Instruction instruction) => kxnor(in instruction);
}
