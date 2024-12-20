using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpminsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpminsd_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<int> src = EvaluateXmmFromInstruction(in instruction, 2).AsInt32();
                    Vector128<int> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsInt32();
                    Vector128<int> result = Vector128<int>.Zero;

                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (int)0);
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

            case Code.EVEX_Vpminsd_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<int> src = EvaluateYmmFromInstruction(in instruction, 2).AsInt32();
                    Vector256<int> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsInt32();
                    Vector256<int> result = Vector256<int>.Zero;

                    for (int i = 0; i < Vector256<int>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (int)0);
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

            case Code.EVEX_Vpminsd_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<int> src = EvaluateZmmFromInstruction(in instruction, 2).AsInt32();
                    Vector512<int> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsInt32();
                    Vector512<int> result = Vector512<int>.Zero;

                    for (int i = 0; i < Vector512<int>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (int)0);
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
