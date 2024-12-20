using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpermb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpermb_xmm_k1z_xmm_xmmm128:
                {
                    int count = Vector128<byte>.Count;
                    Vector128<byte> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, byte>();
                    Vector128<byte> result = input1;

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

                        byte elementAtInput2 = input2[i];
                        if (elementAtInput2 < count)
                        {
                            result = result.WithElement(i, input1[elementAtInput2]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpermb_ymm_k1z_ymm_ymmm256:
                {
                    int count = Vector256<byte>.Count;
                    Vector256<byte> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector256<byte> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, byte>();
                    Vector256<byte> result = input1;

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

                        byte elementAtInput2 = input2[i];
                        if (elementAtInput2 < count)
                        {
                            result = result.WithElement(i, input1[elementAtInput2]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpermb_zmm_k1z_zmm_zmmm512:
                {
                    int count = Vector512<byte>.Count;
                    Vector512<byte> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector512<byte> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, byte>();
                    Vector512<byte> result = input1;

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

                        byte elementAtInput2 = input2[i];
                        if (elementAtInput2 < count)
                        {
                            result = result.WithElement(i, input1[elementAtInput2]);
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
