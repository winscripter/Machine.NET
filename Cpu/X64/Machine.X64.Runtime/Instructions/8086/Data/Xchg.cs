using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void xchg(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Xchg_r16_AX:
                {
                    Register r16 = instruction.GetOpRegister(0);

                    ushort r16Value = this.ProcessorRegisters.EvaluateRegisterValue16(r16);
                    ushort ax = this.ProcessorRegisters.Ax;

                    this.ProcessorRegisters.WriteToRegister16(r16, ax);
                    this.ProcessorRegisters.Ax = r16Value;

                    break;
                }

            case Code.Xchg_r32_EAX:
                {
                    Register r32 = instruction.GetOpRegister(0);

                    uint r32Value = this.ProcessorRegisters.EvaluateRegisterValue32(r32);
                    uint eax = this.ProcessorRegisters.Eax;

                    this.ProcessorRegisters.WriteToRegister32(r32, eax);
                    this.ProcessorRegisters.Eax = r32Value;

                    break;
                }

            case Code.Xchg_r64_RAX:
                {
                    Register r64 = instruction.GetOpRegister(0);

                    ulong r64Value = this.ProcessorRegisters.EvaluateRegisterValue64(r64);
                    ulong rax = this.ProcessorRegisters.Rax;

                    this.ProcessorRegisters.WriteToRegister64(r64, rax);
                    this.ProcessorRegisters.Rax = r64Value;

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
