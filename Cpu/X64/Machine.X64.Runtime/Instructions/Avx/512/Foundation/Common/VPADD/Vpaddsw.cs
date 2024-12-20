using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpaddsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpaddsw_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<short> b = EvaluateXmmFromInstruction(in instruction, 2).AsInt16();
                    Vector128<short> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsInt16();

                    Vector128<short> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsInt16();

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
                            result = result.WithElement(i, (short)(a[i] + b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpaddsw_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<short> b = EvaluateYmmFromInstruction(in instruction, 2).AsInt16();
                    Vector256<short> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsInt16();

                    Vector256<short> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsInt16();

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
                            result = result.WithElement(i, (short)(a[i] + b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpaddsw_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<short> b = EvaluateZmmFromInstruction(in instruction, 2).AsInt16();
                    Vector512<short> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsInt16();

                    Vector512<short> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsInt16();

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
                            result = result.WithElement(i, (short)(a[i] + b[i]));
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
