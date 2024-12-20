using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void adox(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Adox_r32_rm32:
                {
                    ulong result = (ulong)this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0)) + RMEvaluate32(in instruction, 1);
                    this.ProcessorRegisters.RFlagsOF = result > uint.MaxValue;
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)result);
                    break;
                }

            case Code.Adox_r64_rm64:
                {
                    Int128 result = (Int128)this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0)) + RMEvaluate64(in instruction, 1);
                    this.ProcessorRegisters.RFlagsOF = result > ulong.MaxValue;
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
