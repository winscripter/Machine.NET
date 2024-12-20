using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpcompressd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpcompressd_xmmm128_k1z_xmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector128<uint> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetXmm(output, result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpcompressd_ymmm256_k1z_ymm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector256<uint> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetYmm(output, result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpcompressd_zmmm512_k1z_zmm:
                {
                    Register output = instruction.GetOpRegister(0);
                    Vector512<uint> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> result = input;

                    int newVecLength = 0;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            if (!BitUtilities.IsBitSet(opMaskData, i))
                            {
                                result = result.WithElement(newVecLength++, input[i]);
                            }
                        }
                    }

                    ProcessorRegisters.SetZmm(output, result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
