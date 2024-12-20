using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtph2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtph2ps_xmm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        Vector64<Half> vec = this.Memory.ReadBinaryVector64(GetMemOperand64(instruction)).AsHalf();
                        Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));

                        for (int i = 0; i < Vector64<Half>.Count; i++)
                        {
                            if (HasBitSetInK1(i))
                            {
                                if (instruction.ZeroingMasking)
                                {
                                    result = result.WithElement(i, 0F);
                                }
                            }
                            else
                            {
                                result = result.WithElement(i, (float)vec[i]);
                            }
                        }

                        this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    }
                    else
                    {
                        Vector128<Half> vec = EvaluateXmmFromInstruction(in instruction, 1).AsHalf();
                        Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));

                        for (int i = 0; i < Vector128<float>.Count; i++)
                        {
                            if (HasBitSetInK1(i))
                            {
                                if (instruction.ZeroingMasking)
                                {
                                    result = result.WithElement(i, 0F);
                                }
                            }
                            else
                            {
                                result = result.WithElement(i, (float)vec[i]);
                            }
                        }

                        this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    }
                    break;
                }

            case Code.EVEX_Vcvtph2ps_ymm_k1z_xmmm128:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        Vector128<Half> vec = this.Memory.ReadBinaryVector128(GetMemOperand64(instruction)).AsHalf();
                        Vector256<float> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0));

                        for (int i = 0; i < Vector256<float>.Count; i++)
                        {
                            if (HasBitSetInK1(i))
                            {
                                if (instruction.ZeroingMasking)
                                {
                                    result = result.WithElement(i, 0F);
                                }
                            }
                            else
                            {
                                result = result.WithElement(i, (float)vec[i]);
                            }
                        }

                        this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    }
                    else
                    {
                        Vector128<Half> vec = EvaluateXmmFromInstruction(in instruction, 1).AsHalf();
                        Vector256<float> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0));

                        for (int i = 0; i < Vector256<float>.Count; i++)
                        {
                            if (HasBitSetInK1(i))
                            {
                                if (instruction.ZeroingMasking)
                                {
                                    result = result.WithElement(i, 0F);
                                }
                            }
                            else
                            {
                                result = result.WithElement(i, (float)vec[i]);
                            }
                        }

                        this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    }
                    break;
                }

            case Code.EVEX_Vcvtph2ps_zmm_k1z_ymmm256_sae:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        Vector256<Half> vec = this.Memory.ReadBinaryVector256(GetMemOperand64(instruction)).AsHalf();
                        Vector512<float> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));

                        for (int i = 0; i < Vector512<float>.Count; i++)
                        {
                            if (HasBitSetInK1(i))
                            {
                                if (instruction.ZeroingMasking)
                                {
                                    result = result.WithElement(i, 0F);
                                }
                            }
                            else
                            {
                                result = result.WithElement(i, (float)vec[i]);
                            }
                        }

                        this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    }
                    else
                    {
                        Vector256<Half> vec = EvaluateYmmFromInstruction(in instruction, 1).AsHalf();
                        Vector512<float> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));

                        for (int i = 0; i < Vector512<float>.Count; i++)
                        {
                            if (HasBitSetInK1(i))
                            {
                                if (instruction.ZeroingMasking)
                                {
                                    result = result.WithElement(i, 0F);
                                }
                            }
                            else
                            {
                                result = result.WithElement(i, (float)vec[i]);
                            }
                        }

                        this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
