using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpminuw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpminuw_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 2).AsUInt16();
                    Vector128<ushort> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt16();
                    Vector128<ushort> result = Vector128<ushort>.Zero;

                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ushort)0);
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

            case Code.EVEX_Vpminuw_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<ushort> src = EvaluateYmmFromInstruction(in instruction, 2).AsUInt16();
                    Vector256<ushort> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt16();
                    Vector256<ushort> result = Vector256<ushort>.Zero;

                    for (int i = 0; i < Vector256<ushort>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ushort)0);
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

            case Code.EVEX_Vpminuw_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<ushort> src = EvaluateZmmFromInstruction(in instruction, 2).AsUInt16();
                    Vector512<ushort> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt16();
                    Vector512<ushort> result = Vector512<ushort>.Zero;

                    for (int i = 0; i < Vector512<ushort>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (ushort)0);
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
