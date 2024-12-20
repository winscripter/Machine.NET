using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpexpandq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpexpandq_xmm_k1z_xmmm128:
                {
                    Vector128<ulong> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> result = input;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpexpandq_ymm_k1z_ymmm256:
                {
                    Vector256<ulong> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector256<ulong> result = input;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpexpandq_zmm_k1z_zmmm512:
                {
                    Vector512<ulong> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector512<ulong> result = input;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
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
