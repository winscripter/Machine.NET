using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movddup(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movddup_xmm_xmmm64:
                {
                    Vector64<float> vec = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).GetLower(),
                        _ => Vector64<float>.Zero
                    };
                    Vector128<float> result = Vector128.Create(vec, vec);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
