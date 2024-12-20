using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcompresspd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcompresspd_xmmm128_k1z_xmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector128<double> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetXmm(output, result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcompresspd_ymmm256_k1z_ymm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector256<double> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector256<double> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetYmm(output, result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcompresspd_zmmm512_k1z_zmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector512<double> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<double> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetZmm(output, result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
