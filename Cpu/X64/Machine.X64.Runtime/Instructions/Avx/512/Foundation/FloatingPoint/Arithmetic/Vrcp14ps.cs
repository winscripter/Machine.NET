using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrcp14ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrcp14ps_xmm_k1z_xmmm128b32:
                {
                    Vector128<double> src = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    for (int i = 0; i < Vector128<double>.Count; i++)
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

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), src.As<double, float>());
                    break;
                }

            case Code.EVEX_Vrcp14ps_ymm_k1z_ymmm256b32:
                {
                    Vector256<double> src = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    for (int i = 0; i < Vector256<double>.Count; i++)
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

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), src.As<double, float>());
                    break;
                }

            case Code.EVEX_Vrcp14ps_zmm_k1z_zmmm512b32:
                {
                    Vector512<double> src = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, double>();
                    for (int i = 0; i < Vector512<double>.Count; i++)
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

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), src.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
