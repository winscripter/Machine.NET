using Iced.Intel;
using Machine.X64.Component;
using System.IO.Hashing;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void crc32(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Crc32_r32_rm8:
                {
                    uint hash = Crc32.HashToUInt32([RMEvaluate8(in instruction, 1)]);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), hash);
                    break;
                }

            case Code.Crc32_r32_rm16:
                {
                    uint hash = Crc32.HashToUInt32(BitConverter.GetBytes(RMEvaluate16(in instruction, 1)).AsSpan());
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), hash);
                    break;
                }

            case Code.Crc32_r32_rm32:
                {
                    uint hash = Crc32.HashToUInt32(BitConverter.GetBytes(RMEvaluate32(in instruction, 1)).AsSpan());
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), hash);
                    break;
                }

            case Code.Crc32_r64_rm8:
                {
                    uint hash = Crc32.HashToUInt32([RMEvaluate8(in instruction, 1)]);
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), hash);
                    break;
                }

            case Code.Crc32_r64_rm64:
                {
                    uint hash = Crc32.HashToUInt32(BitConverter.GetBytes(RMEvaluate64(in instruction, 1)).AsSpan());
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), hash);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
