using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovsxbw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovsxbw_xmm_xmmm64:
                {
                    Vector64<sbyte> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand(in instruction)).As<float, sbyte>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, sbyte>().GetLower(),
                        _ => Vector64<sbyte>.Zero
                    };
                    Vector128<short> result = Vector128<short>.Zero;
                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        result = result.WithElement(i, src[i]);
                    }
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<short, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
