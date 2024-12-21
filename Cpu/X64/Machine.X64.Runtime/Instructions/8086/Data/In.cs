using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void @in(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.In_AL_DX:
                this.ProcessorRegisters.Al = (byte)this.ReadIOPort(this.ProcessorRegisters.Dx);
                break;

            case Code.In_AL_imm8:
                this.ProcessorRegisters.Al = this.ReadIOPort((byte)instruction.GetImmediate(1));
                break;

            case Code.In_AX_DX:
                this.ProcessorRegisters.Ax = this.ReadIOPort(this.ProcessorRegisters.Dx);
                break;

            case Code.In_AX_imm8:
                this.ProcessorRegisters.Ax = this.ReadIOPort((ushort)instruction.GetImmediate(1));
                break;

            case Code.In_EAX_DX:
                this.ProcessorRegisters.Eax = this.ReadIOPort(this.ProcessorRegisters.Dx);
                break;

            case Code.In_EAX_imm8:
                this.ProcessorRegisters.Eax = this.ReadIOPort((uint)instruction.GetImmediate(1));
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
