using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovsxbd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovsxbd_xmm_xmmm32:
                {
                    Vector64<sbyte> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector32<sbyte>(GetMemOperand64(instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, sbyte>().GetLower(),
                        _ => Vector64<sbyte>.Zero
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
