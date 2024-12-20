using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void rdfsbase(in Instruction instruction)
    {
        this.EnsureFsGsBaseIsAllowed();
        switch (instruction.Code)
        {
            case Code.Rdfsbase_r32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)this.ProcessorRegisters.Fs);
                    break;
                }

            case Code.Rdfsbase_r64:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), this.ProcessorRegisters.Fs);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
