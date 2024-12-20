using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpmaxud(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpmaxud_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<uint> src = EvaluateXmmFromInstruction(in instruction, 2).AsUInt32();
                    Vector128<uint> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt32();
                    Vector128<uint> result = Vector128<uint>.Zero;

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (uint)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, Math.Max(src[i], dst[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpmaxud_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<uint> src = EvaluateYmmFromInstruction(in instruction, 2).AsUInt32();
                    Vector256<uint> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt32();
                    Vector256<uint> result = Vector256<uint>.Zero;

                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (uint)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, Math.Max(src[i], dst[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpmaxud_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<uint> src = EvaluateZmmFromInstruction(in instruction, 2).AsUInt32();
                    Vector512<uint> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt32();
                    Vector512<uint> result = Vector512<uint>.Zero;

                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (uint)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, Math.Max(src[i], dst[i]));
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
