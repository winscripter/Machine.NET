using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermi2pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermi2pd_xmm_k1z_xmm_xmmm128b64:
                {
                    int count = Vector128<double>.Count * 2;
                    Vector128<double> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, double>();
                    Vector128<double> result = input1;

                    Vector128<double> embedded = Permute.Embed(input1, input2, 0D);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0D);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        double elementAtEmbedded = embedded[i];
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

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vpermi2pd_ymm_k1z_ymm_ymmm256b64:
                {
                    int count = Vector256<double>.Count * 2;
                    Vector256<double> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector256<double> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, double>();
                    Vector256<double> result = input1;

                    Vector256<double> embedded = Permute.Embed(input1, input2, 0D);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0D);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        double elementAtEmbedded = embedded[i];
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

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vpermi2pd_zmm_k1z_zmm_zmmm512b64:
                {
                    int count = Vector512<double>.Count * 2;
                    Vector512<double> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<double> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, double>();
                    Vector512<double> result = input1;

                    Vector512<double> embedded = Permute.Embed(input1, input2, 0D);

                    for (int i = 0; i < count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0D);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        double elementAtEmbedded = embedded[i];
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

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
