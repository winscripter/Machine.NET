// This file was auto-generated.
// See /eng/BuildTools/X64/FMA3Generator.

using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void Vfnmsub132ph(in Instruction instruction)
    {
        switch (instruction.Code)
        {
                case Code.EVEX_Vfnmsub132ph_xmm_k1z_xmm_xmmm128b16:
                {
                    Vector128<Half> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, Half>();
                    Vector128<Half> src2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, Half>();

                    Vector128<Half> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, Half>();
                    for (int i = 0; i < Vector128<Half>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                ((Half)(-src1[i]) * result[i]) - src2[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

                case Code.EVEX_Vfnmsub132ph_ymm_k1z_ymm_ymmm256b16:
                {
                    Vector256<Half> src1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, Half>();
                    Vector256<Half> src2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, Half>();

                    Vector256<Half> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, Half>();
                    for (int i = 0; i < Vector256<Half>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                ((Half)(-src1[i]) * result[i]) - src2[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

                case Code.EVEX_Vfnmsub132ph_zmm_k1z_zmm_zmmm512b16_er:
                {
                    Vector512<Half> src1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, Half>();
                    Vector512<Half> src2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, Half>();

                    Vector512<Half> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, Half>();
                    for (int i = 0; i < Vector512<Half>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(
                                i,
                                ((Half)(-src1[i]) * result[i]) - src2[i]);
                        }
                    }

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
