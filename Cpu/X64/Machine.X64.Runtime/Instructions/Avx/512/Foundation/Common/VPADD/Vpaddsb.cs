﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpaddsb(in Instruction instruction) // This is of type 'sbyte'
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpaddsb_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<sbyte> b = EvaluateXmmFromInstruction(in instruction, 2).AsSByte();
                    Vector128<sbyte> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsSByte();

                    Vector128<sbyte> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsSByte();

                    for (int i = 0; i < Vector128<sbyte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (sbyte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (sbyte)(a[i] + b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpaddsb_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<sbyte> b = EvaluateYmmFromInstruction(in instruction, 2).AsSByte();
                    Vector256<sbyte> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsSByte();

                    Vector256<sbyte> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsSByte();

                    for (int i = 0; i < Vector256<sbyte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (sbyte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (sbyte)(a[i] + b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpaddsb_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<sbyte> b = EvaluateZmmFromInstruction(in instruction, 2).AsSByte();
                    Vector512<sbyte> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsSByte();

                    Vector512<sbyte> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsSByte();

                    for (int i = 0; i < Vector512<sbyte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (sbyte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (sbyte)(a[i] + b[i]));
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
