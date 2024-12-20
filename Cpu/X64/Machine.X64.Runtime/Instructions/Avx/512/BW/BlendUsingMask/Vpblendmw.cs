using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpblendmw(in Instruction instruction)
    {
        ReadOnlySpan<bool> controlMask = GetControlMaskMData4();

        switch (instruction.Code)
        {
            case Code.EVEX_Vpblendmw_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<ushort> resultVec = Vector128.Create<ushort>(0);
                    Vector128<ushort> src1 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    Vector128<ushort> src2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, ushort>();

                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVec.As<ushort, float>());

                    break;
                }

            case Code.EVEX_Vpblendmw_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<ushort> resultVec = Vector256.Create<ushort>(0);
                    Vector256<ushort> src1 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    Vector256<ushort> src2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, ushort>();

                    for (int i = 0; i < Vector256<ushort>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), resultVec.As<ushort, float>());

                    break;
                }

            case Code.EVEX_Vpblendmw_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<ushort> resultVec = Vector512.Create<ushort>(0);
                    Vector512<ushort> src1 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, ushort>();
                    Vector512<ushort> src2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, ushort>();

                    for (int i = 0; i < Vector512<ushort>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), resultVec.As<ushort, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
