using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void @out(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Out_DX_AL:
                this.WriteIOPort(this.ProcessorRegisters.Dx, this.ProcessorRegisters.Al);
                break;

            case Code.Out_DX_AX:
                this.WriteIOPort(this.ProcessorRegisters.Dx, this.ProcessorRegisters.Ax);
                break;

            case Code.Out_DX_EAX:
                this.WriteIOPort(this.ProcessorRegisters.Dx, (ushort)this.ProcessorRegisters.Eax);
                break;

            case Code.Out_imm8_AL:
                this.WriteIOPort((byte)instruction.GetImmediate(0), this.ProcessorRegisters.Al);
                break;

            case Code.Out_imm8_AX:
                this.WriteIOPort((byte)instruction.GetImmediate(0), this.ProcessorRegisters.Ax);
                break;

            case Code.Out_imm8_EAX:
                this.WriteIOPort((byte)instruction.GetImmediate(0), this.ProcessorRegisters.Eax);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
