using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtps2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtps2dq_xmm_k1z_xmmm128b32:
                {
                    Vector128<float> vec = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<ulong> result = Vector128.Create((ulong)vec[0], (ulong)vec[1]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vcvtps2dq_ymm_k1z_ymmm256b32:
                {
                    Vector256<float> vec = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<ulong> result = Vector256.Create((ulong)vec[0], (ulong)vec[1], (ulong)vec[2], (ulong)vec[3]);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vcvtps2dq_zmm_k1z_zmmm512b32_er:
                {
                    Vector512<float> vec = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<ulong> result = Vector512.Create((ulong)vec[0], (ulong)vec[1], (ulong)vec[2], (ulong)vec[3], (ulong)vec[4], (ulong)vec[5], (ulong)vec[6], (ulong)vec[7]);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
