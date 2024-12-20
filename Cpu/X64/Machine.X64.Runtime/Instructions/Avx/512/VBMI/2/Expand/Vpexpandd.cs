using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpexpandd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpexpandd_xmm_k1z_xmmm128:
                {
                    Vector128<uint> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> result = input;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0u);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpexpandd_ymm_k1z_ymmm256:
                {
                    Vector256<uint> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> result = input;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0u);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpexpandd_zmm_k1z_zmmm512:
                {
                    Vector512<uint> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> result = input;

                    Register opMask = instruction.OpMask;
                    ulong opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0u);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
