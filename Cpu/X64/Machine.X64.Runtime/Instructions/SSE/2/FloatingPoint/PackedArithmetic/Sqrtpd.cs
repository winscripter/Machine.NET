using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void sqrtpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Sqrtpd_xmm_xmmm128:
                {
                    Vector128<double> parameter2 = EvaluateXmmFromInstruction(in instruction, 1).AsDouble();
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter2.Sqrt().AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
