using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vsqrtpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vsqrtpd_xmm_k1z_xmmm128b64:
                {
                    Vector128<double> parameter2 = EvaluateXmmFromInstruction(in instruction, 1).AsDouble();
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter2.Sqrt().AsSingle());
                    break;
                }

            case Code.EVEX_Vsqrtpd_ymm_k1z_ymmm256b64:
                {
                    Vector256<double> parameter2 = EvaluateYmmFromInstruction(in instruction, 1).AsDouble();
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), parameter2.Sqrt().AsSingle());
                    break;
                }

            case Code.EVEX_Vsqrtpd_zmm_k1z_zmmm512b64_er:
                {
                    Vector512<double> parameter2 = EvaluateZmmFromInstruction(in instruction, 1).AsDouble();
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), parameter2.Sqrt().AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
