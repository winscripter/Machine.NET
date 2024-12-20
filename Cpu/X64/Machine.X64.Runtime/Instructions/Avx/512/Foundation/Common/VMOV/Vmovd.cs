using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovd_rm32_xmm:
            case Code.VEX_Vmovd_rm32_xmm:
                {
                    Vector128<uint> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt32();
                    RMSet32(in instruction, xmm.ToScalar(), 0);
                    break;
                }

            case Code.EVEX_Vmovd_xmm_rm32:
            case Code.VEX_Vmovd_xmm_rm32:
                {
                    uint scalar = RMEvaluate32(in instruction, 1);
                    AlterScalarOfXmm(instruction.GetOpRegister(0), scalar);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
