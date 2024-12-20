using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void wrgsbase(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Wrgsbase_r32:
                {
                    ulong gs = this.ProcessorRegisters.Gs;
                    BitUtilities.SetLower32Bits(ref gs, this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0)));
                    this.ProcessorRegisters.Gs = gs;
                    break;
                }

            case Code.Wrgsbase_r64:
                {
                    this.ProcessorRegisters.Gs = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
