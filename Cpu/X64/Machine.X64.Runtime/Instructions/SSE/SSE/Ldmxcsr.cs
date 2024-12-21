using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ldmxcsr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Ldmxcsr_m32:
                {
                    uint mxcsr = (uint)this.Mxcsr.Value;
                    this.Memory.WriteUInt32(GetMemOperand64(in instruction), mxcsr);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
