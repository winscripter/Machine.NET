// This file was auto-generated.
// See /eng/BuildTools/X64/FMA3Generator.

using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void Vfmsubadd231ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
                case Code.EVEX_Vfmsubadd231ps_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<float> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector128<float> src2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, float>();

                    Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, float>();
                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                i % 2 == 0
                                ? (result[i] * src2[i]) - src1[i]
                                : (result[i] * src2[i]) + src1[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

                case Code.EVEX_Vfmsubadd231ps_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<float> src1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector256<float> src2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, float>();

                    Vector256<float> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, float>();
                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                i % 2 == 0
                                ? (result[i] * src2[i]) - src1[i]
                                : (result[i] * src2[i]) + src1[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

                case Code.EVEX_Vfmsubadd231ps_zmm_k1z_zmm_zmmm512b32_er:
                {
                    Vector512<float> src1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector512<float> src2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, float>();

                    Vector512<float> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, float>();
                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                i % 2 == 0
                                ? (result[i] * src2[i]) - src1[i]
                                : (result[i] * src2[i]) + src1[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
