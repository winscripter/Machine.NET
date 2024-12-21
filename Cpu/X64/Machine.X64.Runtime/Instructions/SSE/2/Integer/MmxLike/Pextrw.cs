using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pextrw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pextrw_r32m16_xmm_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(2);
                    if (imm > 16)
                    {
                        break;
                    }

                    Vector128<ushort> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    ushort value = vec[imm];

                    OpKind opKind = instruction.GetOpKind(0);
                    switch (opKind)
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);
                            break;

                        case OpKind.Memory:
                            this.Memory.WriteUInt16(GetMemOperand(in instruction), value);
                            break;
                    }

                    break;
                }

            case Code.Pextrw_r32_mm_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(2);
                    if (imm > 8)
                    {
                        break;
                    }

                    Vector64<ushort> vec = Vector64.Create<ulong>(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))).As<ulong, ushort>();
                    ushort value = vec[imm];
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);

                    break;
                }

            case Code.Pextrw_r32_xmm_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(2);
                    if (imm > 16)
                    {
                        break;
                    }

                    Vector128<ushort> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    ushort value = vec[imm];

                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);
                    break;
                }

            case Code.Pextrw_r64m16_xmm_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(2);
                    if (imm > 16)
                    {
                        break;
                    }

                    Vector128<ushort> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    ushort value = vec[imm];

                    OpKind opKind = instruction.GetOpKind(0);
                    switch (opKind)
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);
                            break;

                        case OpKind.Memory:
                            this.Memory.WriteUInt16(GetMemOperand(in instruction), value);
                            break;
                    }

                    break;
                }

            case Code.Pextrw_r64_mm_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(2);
                    if (imm > 16)
                    {
                        break;
                    }

                    Vector128<ushort> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    ushort value = vec[imm];

                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);
                    break;
                }

            case Code.Pextrw_r64_xmm_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(2);
                    if (imm > 16)
                    {
                        break;
                    }

                    Vector128<ushort> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    ushort value = vec[imm];

                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
