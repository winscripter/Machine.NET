using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void enter(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Enterw_imm16_imm8:
                {
                    StackPush(this.ProcessorRegisters.Bp);

                    this.ProcessorRegisters.Bp = this.ProcessorRegisters.Sp;
                    ushort localVarSize = (ushort)instruction.GetImmediate(0);
                    this.ProcessorRegisters.Sp -= localVarSize;

                    byte nestingLevel = (byte)instruction.GetImmediate(1);
                    for (int i = 0; i < nestingLevel; i++)
                    {
                        ushort tempBP = this.ProcessorRegisters.Bp;
                        this.ProcessorRegisters.Bp = this.ProcessorRegisters.Sp;
                        this.ProcessorRegisters.Sp -= localVarSize;
                        this.ProcessorRegisters.Bp = tempBP;
                    }

                    break;
                }

            case Code.Enterd_imm16_imm8:
                {
                    StackPush(this.ProcessorRegisters.Ebp);

                    this.ProcessorRegisters.Ebp = this.ProcessorRegisters.Esp;
                    ushort localVarSize = (ushort)instruction.GetImmediate(0);
                    this.ProcessorRegisters.Esp -= localVarSize;

                    byte nestingLevel = (byte)instruction.GetImmediate(1);
                    for (int i = 0; i < nestingLevel; i++)
                    {
                        uint tempEBP = this.ProcessorRegisters.Ebp;
                        this.ProcessorRegisters.Ebp = this.ProcessorRegisters.Esp;
                        this.ProcessorRegisters.Esp -= localVarSize;
                        this.ProcessorRegisters.Ebp = tempEBP;
                    }

                    break;
                }

            case Code.Enterq_imm16_imm8:
                {
                    StackPush(this.ProcessorRegisters.Rbp);

                    this.ProcessorRegisters.Rbp = this.ProcessorRegisters.Rsp;
                    ushort localVarSize = (ushort)instruction.GetImmediate(0);
                    this.ProcessorRegisters.Rsp -= localVarSize;

                    byte nestingLevel = (byte)instruction.GetImmediate(1);
                    for (int i = 0; i < nestingLevel; i++)
                    {
                        ulong tempRBP = this.ProcessorRegisters.Rbp;
                        this.ProcessorRegisters.Rbp = this.ProcessorRegisters.Rsp;
                        this.ProcessorRegisters.Rsp -= localVarSize;
                        this.ProcessorRegisters.Rbp = tempRBP;
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
