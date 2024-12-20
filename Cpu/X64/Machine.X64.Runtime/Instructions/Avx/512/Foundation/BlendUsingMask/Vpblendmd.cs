using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpblendmd(in Instruction instruction)
    {
        ReadOnlySpan<bool> controlMask = GetControlMaskMData4();

        switch (instruction.Code)
        {
            case Code.EVEX_Vpblendmd_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<int> resultVec = Vector128.Create<int>(0);
                    Vector128<int> src1 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, int>();
                    Vector128<int> src2 = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, int>();

                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVec.As<int, float>());

                    break;
                }

            case Code.EVEX_Vpblendmd_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<int> resultVec = Vector256.Create<int>(0);
                    Vector256<int> src1 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, int>();
                    Vector256<int> src2 = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, int>();

                    for (int i = 0; i < Vector256<int>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), resultVec.As<int, float>());

                    break;
                }

            case Code.EVEX_Vpblendmd_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<int> resultVec = Vector512.Create<int>(0);
                    Vector512<int> src1 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, int>();
                    Vector512<int> src2 = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, int>();

                    for (int i = 0; i < Vector512<int>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            resultVec = resultVec.WithElement(i, controlMask[i] ? src1[i] : src2[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), resultVec.As<int, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
