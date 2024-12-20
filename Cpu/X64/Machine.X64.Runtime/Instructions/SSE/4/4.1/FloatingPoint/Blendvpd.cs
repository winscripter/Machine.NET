using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void blendvpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Blendvpd_xmm_xmmm128:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        Blend.VectorsDouble128(
                            this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>(),
                            EvaluateXmmFromInstruction(in instruction, 1).As<float, double>(),
                            this.ProcessorRegisters.Xmm0.As<float, double>()
                        ).As<double, float>()
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
