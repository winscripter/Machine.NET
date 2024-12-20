using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtps2pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtps2pd_xmm_k1z_xmmm64b32:
                {
                    Vector128<float> vec = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<double> result = Vector128.Create((double)vec[0], (double)vec[1]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcvtps2pd_ymm_k1z_xmmm128b32:
                {
                    Vector256<float> vec = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<double> result = Vector256.Create((double)vec[0], (double)vec[1], (double)vec[2], (double)vec[3]);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcvtps2pd_zmm_k1z_ymmm256b32_sae:
                {
                    Vector512<float> vec = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<double> result = Vector512.Create((double)vec[0], (double)vec[1], (double)vec[2], (double)vec[3], (double)vec[4], (double)vec[5], (double)vec[6], (double)vec[7]);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
