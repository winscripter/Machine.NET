using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermi2q(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermi2q_xmm_k1z_xmm_xmmm128b64:
                {
                    int count = Vector128<ulong>.Count * 2;
                    Vector128<ulong> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, ulong>();
                    Vector128<ulong> result = input1;

                    Vector128<ulong> embedded = Permute.Embed(input1, input2, (ulong)0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ulong)0);
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

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpermi2q_ymm_k1z_ymm_ymmm256b64:
                {
                    int count = Vector256<float>.Count * 2;
                    Vector256<ulong> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector256<ulong> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, ulong>();
                    Vector256<ulong> result = input1;

                    Vector256<ulong> embedded = Permute.Embed(input1, input2, (ulong)0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ulong)0);
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

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpermi2q_zmm_k1z_zmm_zmmm512b64:
                {
                    int count = Vector512<float>.Count * 2;
                    Vector512<ulong> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector512<ulong> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, ulong>();
                    Vector512<ulong> result = input1;

                    Vector512<ulong> embedded = Permute.Embed(input1, input2, (ulong)0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ulong)0);
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

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
