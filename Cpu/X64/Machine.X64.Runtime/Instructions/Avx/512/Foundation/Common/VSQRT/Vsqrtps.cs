using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vsqrtps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vsqrtps_xmm_k1z_xmmm128b32:
                {
                    Vector128<float> parameter2 = EvaluateXmmFromInstruction(in instruction, 1);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter2.Sqrt());
                    break;
                }

            case Code.EVEX_Vsqrtps_ymm_k1z_ymmm256b32:
                {
                    Vector256<float> parameter2 = EvaluateYmmFromInstruction(in instruction, 1);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), parameter2.Sqrt());
                    break;
                }

            case Code.EVEX_Vsqrtps_zmm_k1z_zmmm512b32_er:
                {
                    Vector512<float> parameter2 = EvaluateZmmFromInstruction(in instruction, 1);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), parameter2.Sqrt());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
