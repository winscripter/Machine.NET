using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpcompressq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpcompressq_xmmm128_k1z_xmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector128<ulong> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetXmm(output, result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpcompressq_ymmm256_k1z_ymm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector256<ulong> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector256<ulong> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetYmm(output, result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpcompressq_zmmm512_k1z_zmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector512<ulong> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector512<ulong> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetZmm(output, result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
