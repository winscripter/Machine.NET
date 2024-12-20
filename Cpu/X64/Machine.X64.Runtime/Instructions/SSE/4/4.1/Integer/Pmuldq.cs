using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmuldq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmuldq_xmm_xmmm128:
                {
                    Vector128<ulong> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    Vector128<ulong> b = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();

                    Vector128<ulong> result = Vector128.Create(
                        a.GetElement(0) * b.GetElement(0),
                        a.GetElement(1) * b.GetElement(1)
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
