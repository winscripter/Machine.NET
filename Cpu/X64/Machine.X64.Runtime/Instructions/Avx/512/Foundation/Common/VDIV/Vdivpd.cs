using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vdivpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vdivpd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<double> p1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsDouble();
                    Vector128<double> p2 = EvaluateXmmFromInstruction(in instruction, 2).AsDouble();
                    Vector128<double> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsDouble();

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(0, 0D);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, p1[i] / p2[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vdivpd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<double> p1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsDouble();
                    Vector256<double> p2 = EvaluateYmmFromInstruction(in instruction, 2).AsDouble();
                    Vector256<double> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsDouble();

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(0, 0D);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, p1[i] / p2[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vdivpd_zmm_k1z_zmm_zmmm512b64_er:
                {
                    Vector512<double> p1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsDouble();
                    Vector512<double> p2 = EvaluateZmmFromInstruction(in instruction, 2).AsDouble();
                    Vector512<double> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsDouble();

                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(0, 0D);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, p1[i] / p2[i]);
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
