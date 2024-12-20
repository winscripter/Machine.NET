// VPMINSB-VPMINSD-VPMINSQ-VPMINSW: Minimum of Packed Signed Integers
// Basically, all values of the result vector are calculated in this formula:
// Math.Min(src[i], dst[i])
// The result is stored in the destination operand.
// Apply the K1z logic to the result vector.

using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpminsb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpminsb_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<sbyte> src = EvaluateXmmFromInstruction(in instruction, 2).AsSByte();
                    Vector128<sbyte> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsSByte();
                    Vector128<sbyte> result = Vector128<sbyte>.Zero;

                    for (int i = 0; i < Vector128<sbyte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (sbyte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, Math.Min(src[i], dst[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpminsb_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<sbyte> src = EvaluateYmmFromInstruction(in instruction, 2).AsSByte();
                    Vector256<sbyte> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsSByte();
                    Vector256<sbyte> result = Vector256<sbyte>.Zero;

                    for (int i = 0; i < Vector256<sbyte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (sbyte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, Math.Min(src[i], dst[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpminsb_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<sbyte> src = EvaluateZmmFromInstruction(in instruction, 2).AsSByte();
                    Vector512<sbyte> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsSByte();
                    Vector512<sbyte> result = Vector512<sbyte>.Zero;

                    for (int i = 0; i < Vector512<sbyte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (sbyte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, Math.Min(src[i], dst[i]));
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
