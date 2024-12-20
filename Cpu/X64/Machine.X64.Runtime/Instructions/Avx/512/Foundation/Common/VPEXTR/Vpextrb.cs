using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpextrb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpextrb_r32m8_xmm_imm8:
                {
                    byte index = (byte)instruction.GetImmediate(2);
                    if (index > 16)
                    {
                        break;
                    }

                    Vector128<byte> src = EvaluateXmmFromInstruction(in instruction, 1).AsByte();
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            {
                                uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));
                                BitUtilities.SetLower8Bits(ref r32, src[index]);
                                this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), r32);
                                break;
                            }

                        case OpKind.Memory:
                            {
                                this.Memory[GetMemOperand8(instruction)] = src[index];
                                break;
                            }
                    }

                    break;
                }

            case Code.EVEX_Vpextrb_r64m8_xmm_imm8:
                {
                    byte index = (byte)instruction.GetImmediate(2);
                    if (index > 16)
                    {
                        break;
                    }

                    Vector128<byte> src = EvaluateXmmFromInstruction(in instruction, 1).AsByte();
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            {
                                ulong r64 = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0));
                                BitUtilities.SetLower8Bits(ref r64, src[index]);
                                this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), r64);
                                break;
                            }

                        case OpKind.Memory:
                            {
                                this.Memory[GetMemOperand8(instruction)] = src[index];
                                break;
                            }
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
