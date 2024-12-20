using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpblendmb(in Instruction instruction)
    {
        ReadOnlySpan<bool> controlMask = GetControlMaskMData4();

        switch (instruction.Code)
        {
            case Code.EVEX_Vblendmpd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<byte> resultVec = Vector128.Create<byte>(0);
                    Vector128<byte> src1 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, byte>();
                    Vector128<byte> src2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, byte>();

                    for (int i = 0; i < Vector128<byte>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVec.As<byte, float>());

                    break;
                }

            case Code.EVEX_Vblendmpd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<byte> resultVec = Vector256.Create<byte>(0);
                    Vector256<byte> src1 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, byte>();
                    Vector256<byte> src2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, byte>();

                    for (int i = 0; i < Vector256<byte>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), resultVec.As<byte, float>());

                    break;
                }

            case Code.EVEX_Vblendmpd_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<byte> resultVec = Vector512.Create<byte>(0);
                    Vector512<byte> src1 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, byte>();
                    Vector512<byte> src2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, byte>();

                    for (int i = 0; i < Vector512<byte>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), resultVec.As<byte, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
