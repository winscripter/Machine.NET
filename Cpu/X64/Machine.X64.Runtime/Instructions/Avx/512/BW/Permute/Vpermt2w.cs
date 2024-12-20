using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermt2w(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermt2w_xmm_k1z_xmm_xmmm128:
                {
                    int count = Vector128<ushort>.Count * 2;
                    Vector128<ushort> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, ushort>();
                    Vector128<ushort> result = input1;

                    Vector128<ushort> embedded = Permute.Embed<ushort>(input1, input2, 0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ushort)0);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        ushort elementAtEmbedded = embedded[i];
                        if (elementAtEmbedded < count)
                        {
                            if (elementAtEmbedded >= 16)
                            {
                                result = result.WithElement(i, input2[elementAtEmbedded]);
                            }
                            else
                            {
                                result = result.WithElement(i, input1[elementAtEmbedded]);
                            }
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpermt2w_ymm_k1z_ymm_ymmm256:
                {
                    int count = Vector256<ushort>.Count * 2;
                    Vector256<ushort> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector256<ushort> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, ushort>();
                    Vector256<ushort> result = input1;

                    Vector256<ushort> embedded = Permute.Embed<ushort>(input1, input2, 0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ushort)0);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        ushort elementAtEmbedded = embedded[i];
                        if (elementAtEmbedded < count)
                        {
                            if (elementAtEmbedded >= 16)
                            {
                                result = result.WithElement(i, input2[elementAtEmbedded]);
                            }
                            else
                            {
                                result = result.WithElement(i, input1[elementAtEmbedded]);
                            }
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpermt2w_zmm_k1z_zmm_zmmm512:
                {
                    int count = Vector512<ushort>.Count * 2;
                    Vector512<ushort> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector512<ushort> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, ushort>();
                    Vector512<ushort> result = input1;

                    Vector512<ushort> embedded = Permute.Embed<ushort>(input1, input2, 0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ushort)0);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        ushort elementAtEmbedded = embedded[i];
                        if (elementAtEmbedded < count)
                        {
                            if (elementAtEmbedded >= 16)
                            {
                                result = result.WithElement(i, input2[elementAtEmbedded]);
                            }
                            else
                            {
                                result = result.WithElement(i, input1[elementAtEmbedded]);
                            }
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
