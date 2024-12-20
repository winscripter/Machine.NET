using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdrand(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rdrand_r16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)Hrng.GenerateRandom());
                    break;
                }

            case Code.Rdrand_r32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)Hrng.GenerateRandom());
                    break;
                }

            case Code.Rdrand_r64:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)Hrng.GenerateRandom());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
