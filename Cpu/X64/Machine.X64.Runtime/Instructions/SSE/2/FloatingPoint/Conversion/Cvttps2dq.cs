using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvttps2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvttps2dq_xmm_xmmm128:
                {
                    Vector128<float> singlePrecision = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<uint> result = Vector128.Create(
                        (uint)MathF.Round(singlePrecision[0]),
                        (uint)MathF.Round(singlePrecision[1]),
                        (uint)MathF.Round(singlePrecision[2]),
                        (uint)MathF.Round(singlePrecision[3]));
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
