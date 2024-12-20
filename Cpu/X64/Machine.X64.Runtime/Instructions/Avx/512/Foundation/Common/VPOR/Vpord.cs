using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpord(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpord_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<int> b = EvaluateXmmFromInstruction(in instruction, 2).AsInt32();
                    Vector128<int> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsInt32();

                    Vector128<int> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsInt32();

                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0);
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

            case Code.EVEX_Vpord_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<int> b = EvaluateYmmFromInstruction(in instruction, 2).AsInt32();
                    Vector256<int> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsInt32();

                    Vector256<int> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsInt32();

                    for (int i = 0; i < Vector256<int>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0);
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

            case Code.EVEX_Vpord_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<int> b = EvaluateZmmFromInstruction(in instruction, 2).AsInt32();
                    Vector512<int> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsInt32();

                    Vector512<int> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsInt32();

                    for (int i = 0; i < Vector512<int>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0);
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
