using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermi2d(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermi2d_xmm_k1z_xmm_xmmm128b32:
                {
                    int count = Vector128<uint>.Count * 2;
                    Vector128<uint> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, uint>();
                    Vector128<uint> result = input1;

                    Vector128<uint> embedded = Permute.Embed(input1, input2, (uint)0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (uint)0);
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

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpermi2d_ymm_k1z_ymm_ymmm256b32:
                {
                    int count = Vector256<float>.Count * 2;
                    Vector256<uint> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, uint>();
                    Vector256<uint> result = input1;

                    Vector256<uint> embedded = Permute.Embed(input1, input2, (uint)0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (uint)0);
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

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpermi2d_zmm_k1z_zmm_zmmm512b32:
                {
                    int count = Vector512<float>.Count * 2;
                    Vector512<uint> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, uint>();
                    Vector512<uint> result = input1;

                    Vector512<uint> embedded = Permute.Embed(input1, input2, (uint)0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (uint)0);
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

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
