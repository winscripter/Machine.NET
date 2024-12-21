using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void mov(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Mov_AL_moffs8:
                {
                    this.ProcessorRegisters.Al = this.Memory[GetMemOperand(in instruction)];
                    break;
                }

            case Code.Mov_AX_moffs16:
                {
                    this.ProcessorRegisters.Ax = this.Memory.ReadUInt16(GetMemOperand(in instruction));
                    break;
                }

            case Code.Mov_cr_r32:
                {
                    this.ProcessorRegisters.SetCR(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateRegisterValue32(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_cr_r64:
                {
                    this.ProcessorRegisters.SetCR(
                       instruction.GetOpRegister(0),
                       this.ProcessorRegisters.EvaluateRegisterValue64(
                           instruction.GetOpRegister(1)
                       )
                   );
                    break;
                }

            case Code.Mov_dr_r32:
                {
                    this.ProcessorRegisters.SetDR(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateRegisterValue32(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_dr_r64:
                {
                    this.ProcessorRegisters.SetDR(
                       instruction.GetOpRegister(0),
                       this.ProcessorRegisters.EvaluateRegisterValue64(
                           instruction.GetOpRegister(1)
                       )
                    );
                    break;
                }

            case Code.Mov_EAX_moffs32:
                {
                    this.ProcessorRegisters.Eax = this.Memory.ReadUInt32(GetMemOperand(in instruction));
                    break;
                }

            case Code.Mov_moffs16_AX:
                {
                    this.Memory.WriteUInt16(GetMemOperand(in instruction), this.ProcessorRegisters.Ax);
                    break;
                }

            case Code.Mov_moffs32_EAX:
                {
                    this.Memory.WriteUInt32(GetMemOperand(in instruction), this.ProcessorRegisters.Eax);
                    break;
                }

            case Code.Mov_moffs64_RAX:
                {
                    this.Memory.WriteUInt64(GetMemOperand(in instruction), this.ProcessorRegisters.Rax);
                    break;
                }

            case Code.Mov_moffs8_AL:
                {
                    this.Memory[GetMemOperand(in instruction)] = this.ProcessorRegisters.Al;
                    break;
                }

            case Code.Mov_r16_imm16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_r16_rm16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), RMEvaluate16(in instruction, 1));
                    break;
                }

            case Code.Mov_r32m16_Sreg:
                {
                    OpKind kind = instruction.GetOpKind(0);
                    if (kind == OpKind.Register)
                    {
                        this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)this.ProcessorRegisters.EvaluateSReg(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        this.Memory.WriteUInt16(GetMemOperand(in instruction), (ushort)this.ProcessorRegisters.EvaluateSReg(instruction.GetOpRegister(1)));
                    }
                    break;
                }

            case Code.Mov_r32_cr:
                {
                    this.ProcessorRegisters.WriteToRegister32(
                        instruction.GetOpRegister(0),
                        (uint)this.ProcessorRegisters.EvaluateCR(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_r32_dr:
                {
                    this.ProcessorRegisters.WriteToRegister32(
                        instruction.GetOpRegister(0),
                        (uint)this.ProcessorRegisters.EvaluateDR(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_r32_imm32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_r32_rm32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), RMEvaluate32(in instruction, 1));
                    break;
                }

            case Code.Mov_r32_tr:
                {
                    this.ProcessorRegisters.WriteToRegister32(
                        instruction.GetOpRegister(0),
                        (uint)this.ProcessorRegisters.EvaluateTR(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_r64m16_Sreg:
                {
                    OpKind kind = instruction.GetOpKind(0);
                    if (kind == OpKind.Register)
                    {
                        this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)this.ProcessorRegisters.EvaluateSReg(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        this.Memory.WriteUInt16(GetMemOperand(in instruction), (ushort)this.ProcessorRegisters.EvaluateSReg(instruction.GetOpRegister(1)));
                    }
                    break;
                }

            case Code.Mov_r64_cr:
                {
                    this.ProcessorRegisters.WriteToRegister64(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateCR(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_r64_dr:
                {
                    this.ProcessorRegisters.WriteToRegister64(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateDR(
                            instruction.GetOpRegister(1)
                        )
                    );
                    break;
                }

            case Code.Mov_r64_imm64:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_r64_rm64:
                {
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), RMEvaluate64(in instruction, 1));
                    break;
                }

            case Code.Mov_r8_imm8:
                {
                    this.ProcessorRegisters.WriteToRegister8(instruction.GetOpRegister(0), (byte)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_r8_rm8:
                {
                    this.ProcessorRegisters.WriteToRegister8(instruction.GetOpRegister(0), RMEvaluate8(in instruction, 1));
                    break;
                }

            case Code.Mov_RAX_moffs64:
                {
                    this.ProcessorRegisters.Rax = GetMemOperand(in instruction);
                    break;
                }

            case Code.Mov_rm16_imm16:
                {
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_rm16_r16:
                {
                    RMSet16(in instruction, this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1)));
                    break;
                }

            case Code.Mov_rm16_Sreg:
                {
                    RMSet16(in instruction, (ushort)this.ProcessorRegisters.EvaluateSReg(instruction.GetOpRegister(1)));
                    break;
                }

            case Code.Mov_rm32_imm32:
                {
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_rm32_r32:
                {
                    RMSet32(in instruction, this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1)));
                    break;
                }

            case Code.Mov_rm64_imm32:
                {
                    RMSet64(in instruction, (uint)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_rm64_r64:
                {
                    RMSet64(in instruction, this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1)));
                    break;
                }

            case Code.Mov_rm8_imm8:
                {
                    RMSet8(in instruction, (byte)instruction.GetImmediate(1));
                    break;
                }

            case Code.Mov_rm8_r8:
                {
                    RMSet8(in instruction, this.ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(1)));
                    break;
                }

            case Code.Mov_Sreg_r32m16:
                {
                    this.ProcessorRegisters.SetSReg(
                        instruction.GetOpRegister(0),
                        instruction.GetOpKind(1) == OpKind.Register
                            ? this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1))
                            : this.Memory.ReadUInt16(GetMemOperand(in instruction))
                    );
                    break;
                }

            case Code.Mov_Sreg_r64m16:
                {
                    this.ProcessorRegisters.SetSReg(
                        instruction.GetOpRegister(0),
                        instruction.GetOpKind(1) == OpKind.Register
                            ? this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1))
                            : this.Memory.ReadUInt16(GetMemOperand(in instruction))
                    );
                    break;
                }

            case Code.Mov_Sreg_rm16:
                {
                    this.ProcessorRegisters.SetSReg(
                        instruction.GetOpRegister(0),
                        RMEvaluate16(in instruction, 1)
                    );
                    break;
                }

            case Code.Mov_tr_r32:
                {
                    this.ProcessorRegisters.SetTR(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1))
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic); break;
        }
    }
}
