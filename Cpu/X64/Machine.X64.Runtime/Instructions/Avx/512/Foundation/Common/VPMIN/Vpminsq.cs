using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpminsq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpminsq_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<long> src = EvaluateXmmFromInstruction(in instruction, 2).AsInt64();
                    Vector128<long> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsInt64();
                    Vector128<long> result = Vector128<long>.Zero;

                    for (int i = 0; i < Vector128<long>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (long)0);
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

            case Code.EVEX_Vpminsq_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<long> src = EvaluateYmmFromInstruction(in instruction, 2).AsInt64();
                    Vector256<long> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsInt64();
                    Vector256<long> result = Vector256<long>.Zero;

                    for (int i = 0; i < Vector256<long>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (long)0);
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

            case Code.EVEX_Vpminsq_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<long> src = EvaluateZmmFromInstruction(in instruction, 2).AsInt64();
                    Vector512<long> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsInt64();
                    Vector512<long> result = Vector512<long>.Zero;

                    for (int i = 0; i < Vector512<long>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (long)0);
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
