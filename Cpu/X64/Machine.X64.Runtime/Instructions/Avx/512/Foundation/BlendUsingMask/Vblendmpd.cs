using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vblendmpd(in Instruction instruction)
    {
        ReadOnlySpan<bool> controlMask = GetControlMaskMData4();

        switch (instruction.Code)
        {
            case Code.EVEX_Vblendmpd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<double> resultVec = Vector128.Create<double>(0D);
                    Vector128<double> src1 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> src2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, double>();

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVec.As<double, float>());

                    break;
                }

            case Code.EVEX_Vblendmpd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<double> resultVec = Vector256.Create<double>(0D);
                    Vector256<double> src1 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector256<double> src2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, double>();

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), resultVec.As<double, float>());

                    break;
                }

            case Code.EVEX_Vblendmpd_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<double> resultVec = Vector512.Create<double>(0D);
                    Vector512<double> src1 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector512<double> src2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, double>();

                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), resultVec.As<double, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
