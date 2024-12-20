using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtpd2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtpd2ps_xmm_xmmm128:
                {
                    Vector128<double> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<float> result = Vector128.Create((float)vec[0], (float)vec[1], 0F, 0F);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
