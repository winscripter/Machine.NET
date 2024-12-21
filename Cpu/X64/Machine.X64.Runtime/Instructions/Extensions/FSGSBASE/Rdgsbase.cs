using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdgsbase(in Instruction instruction)
    {
        if (this.EnsureFsGsBaseIsAllowed())
        {
            return;
        }

        switch (instruction.Code)
        {
            case Code.Rdgsbase_r32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)this.ProcessorRegisters.Gs);
                    break;
                }

            case Code.Rdgsbase_r64:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), this.ProcessorRegisters.Gs);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
