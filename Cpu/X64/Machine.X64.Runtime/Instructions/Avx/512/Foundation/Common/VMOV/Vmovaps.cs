using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovaps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovaps_xmmm128_k1z_xmm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(in instruction);
                        if (operand % 16 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
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

            case Code.EVEX_Vmovaps_xmm_k1z_xmmm128:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(in instruction);
                        if (operand % 16 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
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

            case Code.EVEX_Vmovaps_ymmm256_k1z_ymm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(in instruction);
                        if (operand % 32 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
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

            case Code.EVEX_Vmovaps_ymm_k1z_ymmm256:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(in instruction);
                        if (operand % 32 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
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

            case Code.EVEX_Vmovaps_zmmm512_k1z_zmm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(in instruction);
                        if (operand % 64 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
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

            case Code.EVEX_Vmovaps_zmm_k1z_zmmm512:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(in instruction);
                        if (operand % 64 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
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
