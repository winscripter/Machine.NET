using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpaddb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpaddb_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<byte> b = EvaluateXmmFromInstruction(in instruction, 2).AsByte();
                    Vector128<byte> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsByte();

                    Vector128<byte> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsByte();

                    for (int i = 0; i < Vector128<byte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (byte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (byte)(a[i] + b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpaddb_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<byte> b = EvaluateYmmFromInstruction(in instruction, 2).AsByte();
                    Vector256<byte> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsByte();

                    Vector256<byte> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsByte();

                    for (int i = 0; i < Vector256<byte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (byte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (byte)(a[i] + b[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpaddb_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<byte> b = EvaluateZmmFromInstruction(in instruction, 2).AsByte();
                    Vector512<byte> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsByte();

                    Vector512<byte> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsByte();

                    for (int i = 0; i < Vector512<byte>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, (byte)0);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (byte)(a[i] + b[i]));
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
