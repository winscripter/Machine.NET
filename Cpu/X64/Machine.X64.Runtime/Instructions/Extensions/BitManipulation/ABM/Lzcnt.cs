using Iced.Intel;
using Machine.X64.Component;
using System.Numerics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void lzcnt(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Lzcnt_r16_rm16:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 1);
                    ushort popcnt = (ushort)BitOperations.LeadingZeroCount(rm16);
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), rm16);
                    break;
                }

            case Code.Lzcnt_r32_rm32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 1);
                    uint popcnt = (uint)BitOperations.LeadingZeroCount(rm32);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), rm32);
                    break;
                }

            case Code.Lzcnt_r64_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    ulong popcnt = (ulong)BitOperations.LeadingZeroCount(rm64);
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), rm64);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
