using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void push(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Push_r16:
                StackPush(this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(0)));
                break;

            case Code.Push_r32:
                StackPush(this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0)));
                break;

            case Code.Push_r64:
                StackPush(this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0)));
                break;

            case Code.Push_rm16:
                StackPush(RMEvaluate16(in instruction));
                break;

            case Code.Push_rm32:
                StackPush(RMEvaluate32(in instruction));
                break;

            case Code.Push_rm64:
                StackPush(RMEvaluate64(in instruction));
                break;

            case Code.Push_imm16:
                StackPush((ushort)instruction.GetImmediate(0));
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
