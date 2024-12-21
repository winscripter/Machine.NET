using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovsxdq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovsxdq_xmm_xmmm64:
                {
                    Vector64<int> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand(in instruction)).As<float, int>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, int>().GetLower(),
                        _ => Vector64<int>.Zero
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
