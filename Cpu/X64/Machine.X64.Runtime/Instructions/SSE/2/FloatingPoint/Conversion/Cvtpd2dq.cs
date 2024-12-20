using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtpd2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtpd2dq_xmm_xmmm128:
                {
                    Vector128<double> vec = EvaluateXmmFromInstruction(in instruction, 1).AsDouble();
                    Vector128<ulong> destinationXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    destinationXmm = destinationXmm.WithElement(0, (ulong)vec[0])
                                                   .WithElement(1, (ulong)vec[1]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), destinationXmm.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
