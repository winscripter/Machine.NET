using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpsubq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpsubq_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<ulong> b = EvaluateXmmFromInstruction(in instruction, 2).AsUInt64();
                    Vector128<ulong> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt64();

                    Vector128<ulong> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsUInt64();

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0uL);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i] - b[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpsubq_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<ulong> b = EvaluateYmmFromInstruction(in instruction, 2).AsUInt64();
                    Vector256<ulong> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt64();

                    Vector256<ulong> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsUInt64();

                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0uL);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i] - b[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpsubq_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<ulong> b = EvaluateZmmFromInstruction(in instruction, 2).AsUInt64();
                    Vector512<ulong> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt64();

                    Vector512<ulong> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsUInt64();

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0uL);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, a[i] - b[i]);
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
