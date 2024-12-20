using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pminud(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pminud_xmm_xmmm128:
                {
                    Vector128<uint> src = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();
                    Vector128<uint> dst = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();

                    Vector128<uint> result = Vector128<uint>.Zero;
                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Min(src[i], dst[i]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
