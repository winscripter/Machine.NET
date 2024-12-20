using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void sahf(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Sahf:
                {
                    this.ProcessorRegisters.Flags = (ushort)this.ProcessorRegisters.Ah;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
