using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermt2b(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermt2b_xmm_k1z_xmm_xmmm128:
                {
                    int count = Vector128<byte>.Count * 2;
                    Vector128<byte> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, byte>();
                    Vector128<byte> result = input1;

                    Vector128<byte> embedded = Permute.Embed<byte>(input1, input2, 0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (byte)0);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        byte elementAtEmbedded = embedded[i];
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

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpermt2b_ymm_k1z_ymm_ymmm256:
                {
                    int count = Vector256<byte>.Count * 2;
                    Vector256<byte> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector256<byte> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, byte>();
                    Vector256<byte> result = input1;

                    Vector256<byte> embedded = Permute.Embed<byte>(input1, input2, 0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (byte)0);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        byte elementAtEmbedded = embedded[i];
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

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpermt2b_zmm_k1z_zmm_zmmm512:
                {
                    int count = Vector512<byte>.Count * 2;
                    Vector512<byte> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector512<byte> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, byte>();
                    Vector512<byte> result = input1;

                    Vector512<byte> embedded = Permute.Embed<byte>(input1, input2, 0);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (byte)0);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        byte elementAtEmbedded = embedded[i];
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

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
