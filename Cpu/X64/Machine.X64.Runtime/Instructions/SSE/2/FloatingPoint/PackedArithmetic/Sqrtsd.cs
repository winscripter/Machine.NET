using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void sqrtsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Sqrtsd_xmm_xmmm64:
                {
                    Vector128<double> dest = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    double parameter2 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => double.NaN
                    };
                    double result = Math.Sqrt(parameter2);
                    dest = dest.WithElement(0, result);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), dest.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
