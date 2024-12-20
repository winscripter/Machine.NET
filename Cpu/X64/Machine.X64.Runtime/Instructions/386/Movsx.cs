using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movsx(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movsx_r16_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Movsx_r16_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)BitUtilities.SignExtend(RMEvaluate8(in instruction, 1)));
                    break;
                }

            case Code.Movsx_r32_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)BitUtilities.SignExtend(RMEvaluate16(in instruction, 1)));
                    break;
                }

            case Code.Movsx_r32_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)BitUtilities.SignExtend(RMEvaluate8(in instruction, 1)));
                    break;
                }

            case Code.Movsx_r64_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), BitUtilities.SignExtend(RMEvaluate16(in instruction, 1)));
                    break;
                }

            case Code.Movsx_r64_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), BitUtilities.SignExtend(RMEvaluate8(in instruction, 1)));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
