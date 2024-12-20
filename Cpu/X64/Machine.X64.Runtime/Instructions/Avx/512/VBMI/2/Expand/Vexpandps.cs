using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vexpandps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vexpandps_xmm_k1z_xmmm128:
                {
                    Vector128<float> input = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = input;

                    Register opMask = instruction.OpMask;
                    float opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vexpandps_ymm_k1z_ymmm256:
                {
                    Vector256<float> input = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> result = input;

                    Register opMask = instruction.OpMask;
                    float opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vexpandps_zmm_k1z_zmmm512:
                {
                    Vector512<float> input = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> result = input;

                    Register opMask = instruction.OpMask;
                    float opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
