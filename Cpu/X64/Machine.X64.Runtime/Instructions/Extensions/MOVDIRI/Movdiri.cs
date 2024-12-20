using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movdiri(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movdiri_m32_r32:
                {
                    this.Memory.WriteUInt32(GetMemOperand32(instruction), this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0)));
                    break;
                }

            case Code.Movdiri_m64_r64:
                {
                    this.Memory.WriteUInt64(GetMemOperand64(instruction), this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0)));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
