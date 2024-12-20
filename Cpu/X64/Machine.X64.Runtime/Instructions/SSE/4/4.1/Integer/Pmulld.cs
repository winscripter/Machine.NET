using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmulld(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmulld_xmm_xmmm128:
                {
                    Vector128<uint> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();
                    Vector128<uint> b = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();

                    Vector128<uint> result = Vector128.Create(
                        a.GetElement(0) * b.GetElement(0),
                        a.GetElement(1) * b.GetElement(1),
                        a.GetElement(2) * b.GetElement(2),
                        a.GetElement(3) * b.GetElement(3)
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
