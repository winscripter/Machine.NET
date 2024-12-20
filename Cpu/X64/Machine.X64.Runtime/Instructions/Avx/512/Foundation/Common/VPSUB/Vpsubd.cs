using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpsubd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpsubd_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<uint> b = EvaluateXmmFromInstruction(in instruction, 2).AsUInt32();
                    Vector128<uint> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt32();

                    Vector128<uint> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsUInt32();

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0u);
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

            case Code.EVEX_Vpsubd_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<uint> b = EvaluateYmmFromInstruction(in instruction, 2).AsUInt32();
                    Vector256<uint> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt32();

                    Vector256<uint> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsUInt32();

                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0u);
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

            case Code.EVEX_Vpsubd_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<uint> b = EvaluateZmmFromInstruction(in instruction, 2).AsUInt32();
                    Vector512<uint> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt32();

                    Vector512<uint> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsUInt32();

                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0u);
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
