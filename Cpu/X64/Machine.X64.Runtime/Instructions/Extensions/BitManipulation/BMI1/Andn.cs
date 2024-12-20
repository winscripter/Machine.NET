using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void andn(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Andn_r32_r32_rm32:
                {
                    uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    uint rm32 = RMEvaluate32(in instruction, 2);

                    uint result = ~r32 & rm32;
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.VEX_Andn_r64_r64_rm64:
                {
                    ulong r64 = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));
                    ulong rm64 = RMEvaluate64(in instruction, 2);

                    ulong result = ~r64 & rm64;
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
