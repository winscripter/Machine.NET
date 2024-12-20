using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpandq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpandq_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<ulong> src = EvaluateXmmFromInstruction(in instruction, 2).AsUInt64();
                    Vector128<ulong> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt64();
                    Vector128<ulong> result = Vector128<ulong>.Zero;

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ulong)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, dst[i] & src[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpandq_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<ulong> src = EvaluateYmmFromInstruction(in instruction, 2).AsUInt64();
                    Vector256<ulong> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt64();
                    Vector256<ulong> result = Vector256<ulong>.Zero;

                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ulong)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, dst[i] & src[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpandq_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<ulong> src = EvaluateZmmFromInstruction(in instruction, 2).AsUInt64();
                    Vector512<ulong> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt64();
                    Vector512<ulong> result = Vector512<ulong>.Zero;

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ulong)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, dst[i] & src[i]);
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
