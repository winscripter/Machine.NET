using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vxorpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vxorps_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<double> b = EvaluateXmmFromInstruction(in instruction, 2).AsDouble();
                    Vector128<double> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsDouble();

                    Vector128<double> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsDouble();

                    for (int i = 0; i < Vector128<double>.Count; i++)
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

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vxorps_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<double> b = EvaluateYmmFromInstruction(in instruction, 2).AsDouble();
                    Vector256<double> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsDouble();

                    Vector256<double> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsDouble();

                    for (int i = 0; i < Vector256<double>.Count; i++)
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

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vxorps_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<double> b = EvaluateZmmFromInstruction(in instruction, 2).AsDouble();
                    Vector512<double> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsDouble();

                    Vector512<double> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsDouble();

                    for (int i = 0; i < Vector512<double>.Count; i++)
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

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
