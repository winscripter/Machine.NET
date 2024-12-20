using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vandps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vaddps_xmm_k1z_xmm_xmmm128b32:
                {
                    Vector128<float> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector128<float> b = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, float>();

                    Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, float>();
                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i].And(b[i]));
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0f, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            case Code.EVEX_Vaddps_ymm_k1z_ymm_ymmm256b32:
                {
                    Vector256<float> a = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector256<float> b = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, float>();

                    Vector256<float> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, float>();
                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i].And(b[i]));
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0f, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            case Code.EVEX_Vaddps_zmm_k1z_zmm_zmmm512b32_er:
                {
                    Vector512<float> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector512<float> b = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(2)).As<float, float>();

                    Vector512<float> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, float>();
                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        if (!HasBitSetInK1(i))
                        {
                            result = result.WithElement(i, a[i].And(b[i]));
                        }
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(0f, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
