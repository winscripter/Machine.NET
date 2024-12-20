using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpminsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpminsw_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<short> src = EvaluateXmmFromInstruction(in instruction, 2).AsInt16();
                    Vector128<short> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsInt16();
                    Vector128<short> result = Vector128<short>.Zero;

                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (short)0);
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

            case Code.EVEX_Vpminsw_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<short> src = EvaluateYmmFromInstruction(in instruction, 2).AsInt16();
                    Vector256<short> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsInt16();
                    Vector256<short> result = Vector256<short>.Zero;

                    for (int i = 0; i < Vector256<short>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (short)0);
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

            case Code.EVEX_Vpminsw_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<short> src = EvaluateZmmFromInstruction(in instruction, 2).AsInt16();
                    Vector512<short> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsInt16();
                    Vector512<short> result = Vector512<short>.Zero;

                    for (int i = 0; i < Vector512<short>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (short)0);
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
