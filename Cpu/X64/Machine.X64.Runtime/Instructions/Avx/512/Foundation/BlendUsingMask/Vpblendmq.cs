using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpblendmq(in Instruction instruction)
    {
        ReadOnlySpan<bool> controlMask = GetControlMaskMData4();

        switch (instruction.Code)
        {
            case Code.EVEX_Vpblendmq_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<ulong> resultVec = Vector128.Create<ulong>(0);
                    Vector128<ulong> src1 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    Vector128<ulong> src2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, ulong>();

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVec.As<ulong, float>());

                    break;
                }

            case Code.EVEX_Vpblendmd_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<ulong> resultVec = Vector256.Create<ulong>(0);
                    Vector256<ulong> src1 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    Vector256<ulong> src2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, ulong>();

                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), resultVec.As<ulong, float>());

                    break;
                }

            case Code.EVEX_Vpblendmd_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<ulong> resultVec = Vector512.Create<ulong>(0);
                    Vector512<ulong> src1 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    Vector512<ulong> src2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, ulong>();

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), resultVec.As<ulong, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
