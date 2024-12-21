using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovupd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovupd_xmmm128_k1z_xmm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand(in instruction);
                        Memory.WriteBinaryVector128(operand, ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateXmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            case Code.EVEX_Vmovupd_xmm_k1z_xmmm128:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand(in instruction);
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            Memory.ReadBinaryVector128(operand));
                    }
                    else
                    {
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateXmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            case Code.EVEX_Vmovupd_ymmm256_k1z_ymm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand(in instruction);
                        Memory.WriteBinaryVector256(operand, ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateYmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            case Code.EVEX_Vmovupd_ymm_k1z_ymmm256:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand(in instruction);
                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Memory.ReadBinaryVector256(operand));
                    }
                    else
                    {
                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateYmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            case Code.EVEX_Vmovupd_zmmm512_k1z_zmm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand(in instruction);
                        Memory.WriteBinaryVector512(operand, ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateZmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            case Code.EVEX_Vmovupd_zmm_k1z_zmmm512:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand(in instruction);
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Memory.ReadBinaryVector512(operand));
                    }
                    else
                    {
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateZmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
