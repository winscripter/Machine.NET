using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void popcnt(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Popcnt_r16_rm16:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 1);
                    ushort popcnt = (ushort)BitUtilities.PopCount(rm16);
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), rm16);
                    break;
                }

            case Code.Popcnt_r32_rm32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 1);
                    uint popcnt = (uint)BitUtilities.PopCount(rm32);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), rm32);
                    break;
                }

            case Code.Popcnt_r64_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    ulong popcnt = (ulong)BitUtilities.PopCount(rm64);
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), rm64);
                    break;
                }
            
            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
