using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpxorq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpxorq_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<ulong> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt64();
                    Vector128<ulong> b = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).AsUInt64();

                    Vector128<ulong> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsUInt64();
                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] ^ b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0uL, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpxorq_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<ulong> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsUInt64();
                    Vector256<ulong> b = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).AsUInt64();

                    Vector256<ulong> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsUInt64();
                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] ^ b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0uL, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpxorq_zmm_k1z_zmm_zmmm512b64:
                {
                    Vector512<ulong> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsUInt64();
                    Vector512<ulong> b = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).AsUInt64();

                    Vector512<ulong> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsUInt64();
                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] ^ b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0uL, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
