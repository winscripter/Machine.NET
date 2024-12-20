using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpxord(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpxord_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<uint> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt32();
                    Vector128<uint> b = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).AsUInt32();

                    Vector128<uint> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsUInt32();
                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] ^ b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0u, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpxord_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<uint> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt32();
                    Vector256<uint> b = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).AsUInt32();

                    Vector256<uint> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsUInt32();
                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] ^ b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0u, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpxord_zmm_k1z_zmm_zmmm512b32:
                {
                    Vector512<uint> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt32();
                    Vector512<uint> b = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).AsUInt32();

                    Vector512<uint> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsUInt32();
                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] ^ b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0u, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
