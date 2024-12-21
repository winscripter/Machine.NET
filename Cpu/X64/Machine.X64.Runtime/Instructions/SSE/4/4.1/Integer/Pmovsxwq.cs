using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovsxwq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovsxwq_xmm_xmmm32:
                {
                    Vector64<short> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector32<short>(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, short>().GetLower(),
                        _ => Vector64<short>.Zero
                    };
                    Vector128<long> result = Vector128<long>.Zero;
                    for (int i = 0; i < Vector128<long>.Count; i++)
                    {
                        result = result.WithElement(i, src[i]);
                    }
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<long, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
