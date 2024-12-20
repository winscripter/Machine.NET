using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vdivps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vdivps_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<float> p1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> p2 = EvaluateXmmFromInstruction(in instruction, 2);
                    Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(0, 0F);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, p1[i] / p2[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vdivps_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<float> p1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    Vector256<float> p2 = EvaluateYmmFromInstruction(in instruction, 2);
                    Vector256<float> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0));

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(0, 0F);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, p1[i] / p2[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vdivps_zmm_k1z_zmm_zmmm512b32_er:
                {
                    Vector512<float> p1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1));
                    Vector512<float> p2 = EvaluateZmmFromInstruction(in instruction, 2);
                    Vector512<float> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(0, 0F);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, p1[i] / p2[i]);
                        }
                    }

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
