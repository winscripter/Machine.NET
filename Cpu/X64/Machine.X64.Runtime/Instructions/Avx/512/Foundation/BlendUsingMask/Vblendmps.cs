using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vblendmps(in Instruction instruction)
    {
        ReadOnlySpan<bool> controlMask = GetControlMaskMData4();

        switch (instruction.Code)
        {
            case Code.EVEX_Vblendmpd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<float> resultVec = Vector128.Create<float>(0F);
                    Vector128<float> src1 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> src2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2));

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVec.As<float, float>());

                    break;
                }

            case Code.EVEX_Vblendmpd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<float> resultVec = Vector256.Create<float>(0F);
                    Vector256<float> src1 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    Vector256<float> src2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2));

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), resultVec.As<float, float>());

                    break;
                }

            case Code.EVEX_Vblendmpd_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<float> resultVec = Vector512.Create<float>(0F);
                    Vector512<float> src1 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1));
                    Vector512<float> src2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2));

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), resultVec.As<float, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
