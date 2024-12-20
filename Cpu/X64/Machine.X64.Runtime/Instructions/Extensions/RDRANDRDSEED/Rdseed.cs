using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdseed(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Rdseed_r16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)Hrng.GenerateSeed());
                    break;
                }

            case Code.Rdseed_r32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)Hrng.GenerateSeed());
                    break;
                }

            case Code.Rdseed_r64:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)Hrng.GenerateSeed());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
