using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpminub(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpminub_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<byte> src = EvaluateXmmFromInstruction(in instruction, 2).AsByte();
                    Vector128<byte> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsByte();
                    Vector128<byte> result = Vector128<byte>.Zero;

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
                            result = result.WithElement(i, Math.Min(src[i], dst[i]));
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpminub_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<byte> src = EvaluateYmmFromInstruction(in instruction, 2).AsByte();
                    Vector256<byte> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsByte();
                    Vector256<byte> result = Vector256<byte>.Zero;

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
                            result = result.WithElement(i, Math.Min(src[i], dst[i]));
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vpminub_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<byte> src = EvaluateZmmFromInstruction(in instruction, 2).AsByte();
                    Vector512<byte> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsByte();
                    Vector512<byte> result = Vector512<byte>.Zero;

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
