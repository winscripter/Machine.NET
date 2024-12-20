using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vxorps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vxorps_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<float> b = EvaluateXmmFromInstruction(in instruction, 2);
                    Vector128<float> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    
                    Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    
                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0f);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i].Xor(b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vxorps_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<float> b = EvaluateYmmFromInstruction(in instruction, 2);
                    Vector256<float> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));

                    Vector256<float> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0));

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0f);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i].Xor(b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vxorps_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<float> b = EvaluateZmmFromInstruction(in instruction, 2);
                    Vector512<float> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1));

                    Vector512<float> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0f);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i].Xor(b[i]));
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
