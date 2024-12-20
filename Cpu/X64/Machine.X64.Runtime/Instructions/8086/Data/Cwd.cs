using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cwd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cwd:
                if ((ProcessorRegisters.Ax & 0x8000) == 0x8000)
                {
                    ProcessorRegisters.Dx = 0xFFFF;
                }
                else
                {
                    ProcessorRegisters.Dx = 0;
                }
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
