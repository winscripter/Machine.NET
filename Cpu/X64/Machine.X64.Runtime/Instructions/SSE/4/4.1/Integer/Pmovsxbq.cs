using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovsxbq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovsxbq_xmm_xmmm16:
                {
                    Vector64<sbyte> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector16<sbyte>(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, sbyte>().GetLower(),
                        _ => Vector64<sbyte>.Zero
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
