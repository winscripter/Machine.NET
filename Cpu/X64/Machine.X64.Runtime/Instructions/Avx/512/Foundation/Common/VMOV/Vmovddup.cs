using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovddup(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovddup_xmm_k1z_xmmm64:
                {
                    float scalar = ReadXmmScalarOrSingle(in instruction, 1);
                    Vector128<float> xmm = Vector128.Create([scalar, scalar]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            case Code.EVEX_Vmovddup_ymm_k1z_ymmm256:
                {
                    this.ProcessorRegisters.SetYmm(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)));
                    break;
                }

            case Code.EVEX_Vmovddup_zmm_k1z_zmmm512:
                {
                    this.ProcessorRegisters.SetZmm(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)));
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
