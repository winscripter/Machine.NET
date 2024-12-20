using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmova(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmova_r16_rm16:
                {
                    if ((!this.ProcessorRegisters.RFlagsCF && !this.ProcessorRegisters.RFlagsZF))
                        this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Cmova_r32_rm32:
                {
                    if ((!this.ProcessorRegisters.RFlagsCF && !this.ProcessorRegisters.RFlagsZF))
                        this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), RMEvaluate32(in instruction, 1));
                    break;
                }

            case Code.Cmova_r64_rm64:
                {
                    if ((!this.ProcessorRegisters.RFlagsCF && !this.ProcessorRegisters.RFlagsZF))
                        this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), RMEvaluate64(in instruction, 1));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
