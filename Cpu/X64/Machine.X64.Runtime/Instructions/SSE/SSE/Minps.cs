using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void minps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Minps_xmm_xmmm128:
                {
                    Vector128<float> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> parameter2 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)),
                        _ => Vector128<float>.Zero
                    };
                    Vector128<float> result = VectorMinMax.OfMin(parameter1, parameter2);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
