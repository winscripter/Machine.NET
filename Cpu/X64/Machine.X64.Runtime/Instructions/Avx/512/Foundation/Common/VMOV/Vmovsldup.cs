using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovsldup(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovsldup_xmm_k1z_xmmm128:
                {
                    Vector128<float> xmm = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = Vector128.Create(xmm[1], xmm[1], xmm[3], xmm[3]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vmovsldup_ymm_k1z_ymmm256:
                {
                    Vector256<float> xmm = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> result = Vector256.Create(xmm[1], xmm[1], xmm[1], xmm[1], xmm[3], xmm[3], xmm[3], xmm[3]);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vmovsldup_zmm_k1z_zmmm512:
                {
                    Vector512<float> xmm = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> result = Vector512.Create(xmm[1], xmm[1], xmm[1], xmm[1], xmm[1], xmm[1], xmm[1], xmm[1], xmm[3], xmm[3], xmm[3], xmm[3], xmm[3], xmm[3], xmm[3], xmm[3]);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
