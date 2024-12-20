using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void blsr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Blsr_r32_rm32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 1);
                    uint value = rm32 & (rm32 - 1);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);
                    break;
                }

            case Code.VEX_Blsr_r64_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    ulong value = rm64 & (rm64 - 1);
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
