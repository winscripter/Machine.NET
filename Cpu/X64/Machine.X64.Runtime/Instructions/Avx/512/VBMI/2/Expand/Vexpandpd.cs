using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vexpandpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vexpandpd_xmm_k1z_xmmm128:
                {
                    Vector128<double> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> result = input;

                    Register opMask = instruction.OpMask;
                    double opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vexpandpd_ymm_k1z_ymmm256:
                {
                    Vector256<double> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector256<double> result = input;

                    Register opMask = instruction.OpMask;
                    double opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vexpandpd_zmm_k1z_zmmm512:
                {
                    Vector512<double> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<double> result = input;

                    Register opMask = instruction.OpMask;
                    double opMaskData = GetOpMask(opMask);

                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if ((HasBitSetInK1(i) && instruction.ZeroingMasking) || !BitUtilities.IsBitSet(opMaskData, i))
                        {
                            result = result.WithElement(i, 0uL);
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
