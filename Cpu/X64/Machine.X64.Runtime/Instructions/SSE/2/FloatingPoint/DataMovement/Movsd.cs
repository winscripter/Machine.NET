using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movsd_xmmm64_xmm:
                {
                    double scalar = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar();
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        Memory.WriteDouble(GetMemOperand64(in instruction), scalar);
                    }
                    else
                    {
                        Vector128<double> xmm = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                        xmm = xmm.WithElement(0, scalar);
                        ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm.As<double, float>());
                    }
                    break;
                }

            case Code.Movsd_xmm_xmmm64:
                {
                    double scalar = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Memory.ReadBinaryVector128(GetMemOperand64(in instruction)).As<float, double>().ToScalar(),
                        OpKind.Register => ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => double.NaN
                    };

                    Vector128<double> xmm = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    xmm = xmm.WithElement(0, scalar);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
