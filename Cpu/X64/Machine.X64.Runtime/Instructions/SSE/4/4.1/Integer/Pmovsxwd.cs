using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovsxwd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovsxwd_xmm_xmmm64:
                {
                    Vector64<short> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector32<short>(GetMemOperand64(instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, short>().GetLower(),
                        _ => Vector64<short>.Zero
                    };
                    Vector128<int> result = Vector128<int>.Zero;
                    for (int i = 0; i < Vector128<int>.Count; i++)
                    {
                        result = result.WithElement(i, src[i]);
                    }
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<int, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
