using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pminsb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pminsb_xmm_xmmm128:
                {
                    Vector128<sbyte> src = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, sbyte>();
                    Vector128<sbyte> dst = EvaluateXmmFromInstruction(in instruction, 1).As<float, sbyte>();

                    Vector128<sbyte> result = Vector128<sbyte>.Zero;
                    for (int i = 0; i < Vector128<sbyte>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Min(src[i], dst[i]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<sbyte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
