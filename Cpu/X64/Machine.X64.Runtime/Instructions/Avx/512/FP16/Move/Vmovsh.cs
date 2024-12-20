using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    // wtf does EVEX_Vmovsh_xmm_k1z_xmm_xmm even do
    private void vmovsh(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovsh_m16_k1_xmm:
                {
                    this.Memory.WriteHalf(
                        GetMemOperand64(instruction),
                        this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsHalf().ToScalar()
                    );
                    break;
                }

            case Code.EVEX_Vmovsh_xmm_k1z_m16:
                {
                    AlterScalarOfXmm(
                        instruction.GetOpRegister(0),
                        this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsHalf().ToScalar()
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
