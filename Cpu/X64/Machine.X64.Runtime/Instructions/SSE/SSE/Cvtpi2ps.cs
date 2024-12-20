using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtpi2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtpi2ps_xmm_mmm64:
                {
                    ulong value = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Memory.ReadUInt64(GetMemOperand64(instruction)),
                        OpKind.Register => ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1)),
                        _ => 0uL
                    };
                    uint hi = BitUtilities.GetUpper32Bits(value);
                    uint lo = BitUtilities.GetLower32Bits(value);

                    Vector128<float> vector = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    vector = vector.WithElement(0, hi)
                                   .WithElement(1, lo);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vector);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
