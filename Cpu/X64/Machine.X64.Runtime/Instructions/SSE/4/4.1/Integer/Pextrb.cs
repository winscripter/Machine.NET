using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pextrb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pextrb_r32m8_xmm_imm8:
                {
                    byte imm8 = (byte)instruction.GetImmediate(2);
                    if (imm8 > 15)
                    {
                        RaiseException(StaticErrors.GeneralProtectionFault);
                        break;
                    }

                    Vector128<byte> xmm = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), xmm[imm8]);
                            break;

                        case OpKind.Memory:
                            this.Memory[GetMemOperand8(in instruction)] = xmm[imm8];
                            break;
                    }

                    break;
                }

            case Code.Pextrb_r64m8_xmm_imm8:
                {
                    byte imm8 = (byte)instruction.GetImmediate(2);
                    if (imm8 > 15)
                    {
                        RaiseException(StaticErrors.GeneralProtectionFault);
                        break;
                    }

                    Vector128<byte> xmm = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), xmm[imm8]);
                            break;

                        case OpKind.Memory:
                            this.Memory[GetMemOperand8(in instruction)] = xmm[imm8];
                            break;
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
