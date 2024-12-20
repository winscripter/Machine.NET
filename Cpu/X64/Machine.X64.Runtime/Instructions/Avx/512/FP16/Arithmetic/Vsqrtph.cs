using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vsqrtph(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vsqrtph_xmm_k1z_xmmm128b16:
                {
                    Vector128<Half> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsHalf();

                    Vector128<Half> result = Vector128<Half>.Zero;
                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        result = result.WithElement(i, Half.Sqrt(src1[i]));
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(Half.Zero, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            case Code.EVEX_Vsqrtph_ymm_k1z_ymmm256b16:
                {
                    Vector256<Half> src1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).AsHalf();

                    Vector256<Half> result = Vector256<Half>.Zero;
                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        result = result.WithElement(i, Half.Sqrt(src1[i]));
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(Half.Zero, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            case Code.EVEX_Vsqrtph_zmm_k1z_zmmm512b16_er:
                {
                    Vector512<Half> src1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).AsHalf();

                    Vector512<Half> result = Vector512<Half>.Zero;
                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        result = result.WithElement(i, Half.Sqrt(src1[i]));
                    }

                    if (instruction.OpMask != Register.None && instruction.ZeroingMasking)
                        result = result.K1z(Half.Zero, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
