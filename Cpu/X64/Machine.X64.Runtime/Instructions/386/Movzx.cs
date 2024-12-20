using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movzx(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movzx_r16_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Movzx_r16_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), RMEvaluate8(in instruction, 1));
                    break;
                }

            case Code.Movzx_r32_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Movzx_r32_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), RMEvaluate8(in instruction, 1));
                    break;
                }

            case Code.Movzx_r64_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Movzx_r64_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), RMEvaluate8(in instruction, 1));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
