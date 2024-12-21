using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void fnstsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Fnstsw_AX:
                {
                    this.ProcessorRegisters.Ax = this.Fpu.StatusWord;
                    break;
                }

            case Code.Fnstsw_m2byte:
                {
                    this.Memory.WriteUInt16(GetMemOperand16(instruction), this.Fpu.StatusWord);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
