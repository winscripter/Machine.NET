using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void blsi(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Blsi_r32_rm32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 1);
                    uint result = rm32 & (uint)-rm32;
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.VEX_Blsi_r64_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    ulong result = rm64 & (ulong)-(long)rm64;
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
