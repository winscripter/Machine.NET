using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vporq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vporq_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<long> b = EvaluateXmmFromInstruction(in instruction, 2).AsInt64();
                    Vector128<long> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsInt64();

                    Vector128<long> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsInt64();

                    for (int i = 0; i < Vector128<long>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0L);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i] | b[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vporq_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<long> b = EvaluateYmmFromInstruction(in instruction, 2).AsInt64();
                    Vector256<long> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsInt64();

                    Vector256<long> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsInt64();

                    for (int i = 0; i < Vector256<long>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0L);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i] | b[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vporq_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<long> b = EvaluateZmmFromInstruction(in instruction, 2).AsInt64();
                    Vector512<long> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsInt64();

                    Vector512<long> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsInt64();

                    for (int i = 0; i < Vector512<long>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0L);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i] | b[i]);
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
