using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime 
{
    private void pmaxuw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            // There is no MMX version of PMINUW/PMAXUW
            case Code.Pmaxuw_xmm_xmmm128:
                {
                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();
                    Vector128<ushort> result = Vector128<ushort>.Zero;

                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                        result = result.WithElement(i, src[i] > dst[i] ? src[i] : dst[i]);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
