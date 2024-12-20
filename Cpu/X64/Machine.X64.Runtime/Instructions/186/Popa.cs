using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void popa(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Popaw:
                {
                    this.ProcessorRegisters.Di = StackPopU16();
                    this.ProcessorRegisters.Si = StackPopU16();
                    this.ProcessorRegisters.Bp = StackPopU16();
                    this.ProcessorRegisters.Ax = StackPopU16();
                    this.ProcessorRegisters.Bx = StackPopU16();
                    this.ProcessorRegisters.Dx = StackPopU16();
                    this.ProcessorRegisters.Cx = StackPopU16();
                    this.ProcessorRegisters.Ax = StackPopU16();

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
