using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmovno(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmovno_r16_rm16:
                {
                    if (local_Condition())
                        this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Cmovno_r32_rm32:
                {
                    if (local_Condition())
                        this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), RMEvaluate32(in instruction, 1));
                    break;
                }

            case Code.Cmovno_r64_rm64:
                {
                    if (local_Condition())
                        this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), RMEvaluate64(in instruction, 1));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }

        bool local_Condition()
        {
            return !this.ProcessorRegisters.RFlagsOF;
        }
    }
}
