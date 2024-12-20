using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrcp14pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrcp14pd_xmm_k1z_xmmm128b64:
                {
                    Vector128<float> src = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                src = src.WithElement(i, 0);
                            }
                        }
                        else
                        {
                            src = src.WithElement(i, 1F / src[i]);
                        }
                    }

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), src);
                    break;
                }

            case Code.EVEX_Vrcp14pd_ymm_k1z_ymmm256b64:
                {
                    Vector256<float> src = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                src = src.WithElement(i, 0);
                            }
                        }
                        else
                        {
                            src = src.WithElement(i, 1F / src[i]);
                        }
                    }

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), src);
                    break;
                }

            case Code.EVEX_Vrcp14pd_zmm_k1z_zmmm512b64:
                {
                    Vector512<float> src = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1));
                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                src = src.WithElement(i, 0);
                            }
                        }
                        else
                        {
                            src = src.WithElement(i, 1F / src[i]);
                        }
                    }

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), src);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
