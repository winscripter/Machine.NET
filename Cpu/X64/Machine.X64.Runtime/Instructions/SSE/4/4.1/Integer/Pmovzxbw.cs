using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovzxbw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovzxbw_xmm_xmmm64:
                {
                    Vector64<byte> src = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand64(in instruction)).As<float, byte>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, byte>().GetLower(),
                        _ => Vector64<byte>.Zero
                    };
                    Vector128<ushort> result = Vector128<ushort>.Zero;
                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        result = result.WithElement(i, src[i]);
                    }
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
