using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movss_xmmm32_xmm:
                {
                    float scalar = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar();
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        Memory.WriteSingle(GetMemOperand64(instruction), scalar);
                    }
                    else
                    {
                        Vector128<float> xmm = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                        xmm = xmm.WithElement(0, scalar);
                        ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    }
                    break;
                }

            case Code.Movss_xmm_xmmm32:
                {
                    float scalar = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Memory.ReadBinaryVector128(GetMemOperand64(instruction)).ToScalar(),
                        OpKind.Register => ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar(),
                        _ => float.NaN
                    };

                    Vector128<float> xmm = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    xmm = xmm.WithElement(0, scalar);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
