using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void blendvps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Blendvps_xmm_xmmm128:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        Blend.VectorsSingle128(
                            this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)),
                            EvaluateXmmFromInstruction(in instruction, 1),
                            this.ProcessorRegisters.Xmm0
                        )
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
