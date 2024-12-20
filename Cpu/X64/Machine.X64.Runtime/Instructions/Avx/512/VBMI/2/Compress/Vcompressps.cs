using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcompressps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcompressps_xmmm128_k1z_xmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector128<float> input = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetXmm(output, result);
                    break;
                }

            case Code.EVEX_Vcompressps_ymmm256_k1z_ymm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector256<float> input = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetYmm(output, result);
                    break;
                }

            case Code.EVEX_Vcompressps_zmmm512_k1z_zmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector512<float> input = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetZmm(output, result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
