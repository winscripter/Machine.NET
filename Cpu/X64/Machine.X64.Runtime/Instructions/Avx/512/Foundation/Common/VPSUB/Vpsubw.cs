using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpsubw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpsubw_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<ushort> b = EvaluateXmmFromInstruction(in instruction, 2).AsUInt16();
                    Vector128<ushort> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt16();

                    Vector128<ushort> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsUInt16();

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
                            result = result.WithElement(i, (ushort)(a[i] - b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpsubw_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<ushort> b = EvaluateYmmFromInstruction(in instruction, 2).AsUInt16();
                    Vector256<ushort> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt16();

                    Vector256<ushort> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsUInt16();

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
                            result = result.WithElement(i, (ushort)(a[i] - b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpsubw_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<ushort> b = EvaluateZmmFromInstruction(in instruction, 2).AsUInt16();
                    Vector512<ushort> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt16();

                    Vector512<ushort> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsUInt16();

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
                            result = result.WithElement(i, (ushort)(a[i] - b[i]));
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
