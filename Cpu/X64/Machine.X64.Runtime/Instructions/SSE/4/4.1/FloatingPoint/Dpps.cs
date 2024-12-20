using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void dpps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Dpps_xmm_xmmm128_imm8:
                {
                    Vector128<float> vector = DotProduct.CalculateSingle128(
                        this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)),
                        EvaluateXmmFromInstruction(in instruction, 1),
                        (byte)instruction.GetImmediate(2)
                    );
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vector);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
