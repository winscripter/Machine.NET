using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtpd2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtpd2ps_xmm_k1z_xmmm128b64:
                {
                    Vector128<double> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<float> result = Vector128.Create((float)vec[0], (float)vec[1], 0F, 0F);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vcvtpd2ps_xmm_k1z_ymmm256b64:
                {
                    Vector256<double> vec = EvaluateYmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector256<float> result = Vector256.Create((float)vec[0], (float)vec[1], (float)vec[2], (float)vec[3], 0F, 0F, 0F, 0F);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vcvtpd2ps_ymm_k1z_zmmm512b64_er:
                {
                    Vector512<double> vec = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<float> result = Vector512.Create((float)vec[0], (float)vec[1], (float)vec[2], (float)vec[3], (float)vec[4], (float)vec[5], (float)vec[6], (float)vec[7], 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
