// This file was auto-generated.
// See /eng/BuildTools/X64/FMA3Generator.

using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void Vfmsub213pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
                case Code.EVEX_Vfmsub213pd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<double> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> src2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, double>();

                    Vector128<double> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                (src2[i] * src1[i]) - result[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

                case Code.EVEX_Vfmsub213pd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<double> src1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector256<double> src2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, double>();

                    Vector256<double> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, double>();
                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                (src2[i] * src1[i]) - result[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

                case Code.EVEX_Vfmsub213pd_zmm_k1z_zmm_zmmm512b64_er:
                {
                    Vector512<double> src1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector512<double> src2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, double>();

                    Vector512<double> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, double>();
                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                (src2[i] * src1[i]) - result[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
