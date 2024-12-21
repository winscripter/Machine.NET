using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvttps2pi(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvttps2pi_mm_xmmm64:
                {
                    Vector64<float> vec = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector64(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).GetLower(),
                        _ => Vector64<float>.Zero
                    };
                    ulong mmx = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0));
                    BitUtilities.SetLower32Bits(ref mmx, (uint)MathF.Truncate(vec[0]));
                    BitUtilities.SetUpper32Bits(ref mmx, (uint)MathF.Truncate(vec[1]));
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mmx);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
