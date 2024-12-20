using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmulpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmulpd_xmm_k1z_xmm_xmmm128b64:
                {
                    Vector128<double> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> b = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, double>();

                    Vector128<double> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] * b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0d, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vmulpd_ymm_k1z_ymm_ymmm256b64:
                {
                    Vector256<double> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector256<double> b = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, double>();

                    Vector256<double> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, double>();
                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] * b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0d, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vmulpd_zmm_k1z_zmm_zmmm512b64_er:
                {
                    Vector512<double> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector512<double> b = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, double>();

                    Vector512<double> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, double>();
                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i] * b[i]);
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0d, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
