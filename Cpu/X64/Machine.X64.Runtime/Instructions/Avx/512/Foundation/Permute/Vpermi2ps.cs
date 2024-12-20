using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermi2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermi2ps_xmm_k1z_xmm_xmmm128b32:
                {
                    int count = Vector128<float>.Count * 2;
                    Vector128<float> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, float>();
                    Vector128<float> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, float>();
                    Vector128<float> result = input1;

                    Vector128<float> embedded = Permute.Embed(input1, input2, 0F);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0F);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        float elementAtEmbedded = embedded[i];
                        if (elementAtEmbedded < count)
                        {
                            if (elementAtEmbedded >= 16)
                            {
                                result = result.WithElement(i, input2[(int)elementAtEmbedded]);
                            }
                            else
                            {
                                result = result.WithElement(i, input1[(int)elementAtEmbedded]);
                            }
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            case Code.EVEX_Vpermi2ps_ymm_k1z_ymm_ymmm256b32:
                {
                    int count = Vector256<float>.Count * 2;
                    Vector256<float> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, float>();
                    Vector256<float> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, float>();
                    Vector256<float> result = input1;

                    Vector256<float> embedded = Permute.Embed(input1, input2, 0F);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0F);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        float elementAtEmbedded = embedded[i];
                        if (elementAtEmbedded < count)
                        {
                            if (elementAtEmbedded >= 16)
                            {
                                result = result.WithElement(i, input2[(int)elementAtEmbedded]);
                            }
                            else
                            {
                                result = result.WithElement(i, input1[(int)elementAtEmbedded]);
                            }
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            case Code.EVEX_Vpermi2ps_zmm_k1z_zmm_zmmm512b32:
                {
                    int count = Vector512<float>.Count * 2;
                    Vector512<float> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, float>();
                    Vector512<float> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, float>();
                    Vector512<float> result = input1;

                    Vector512<float> embedded = Permute.Embed(input1, input2, 0F);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0F);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        float elementAtEmbedded = embedded[i];
                        if (elementAtEmbedded < count)
                        {
                            if (elementAtEmbedded >= 16)
                            {
                                result = result.WithElement(i, input2[(int)elementAtEmbedded]);
                            }
                            else
                            {
                                result = result.WithElement(i, input1[(int)elementAtEmbedded]);
                            }
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
