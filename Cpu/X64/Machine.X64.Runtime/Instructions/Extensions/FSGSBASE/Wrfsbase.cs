using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void wrfsbase(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Wrfsbase_r32:
                {
                    ulong fs = this.ProcessorRegisters.Fs;
                    BitUtilities.SetLower32Bits(ref fs, this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0)));
                    this.ProcessorRegisters.Fs = fs;
                    break;
                }

            case Code.Wrfsbase_r64:
                {
                    this.ProcessorRegisters.Fs = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
