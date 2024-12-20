using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvttpd2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvttpd2dq_xmm_xmmm128:
                {
                    Vector128<double> dpVector = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<uint> resultVector = Vector128.Create(
                        (uint)Math.Truncate(dpVector[0]),
                        (uint)Math.Truncate(dpVector[1]),
                        0u,
                        0u);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultVector.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
