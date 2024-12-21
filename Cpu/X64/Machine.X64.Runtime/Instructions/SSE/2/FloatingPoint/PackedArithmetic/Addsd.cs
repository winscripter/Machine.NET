using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void addsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Addsd_xmm_xmmm64:
                {
                    double scalar2 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => double.NaN
                    };
                    Vector128<double> parameter1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    double scalar1 = parameter1.ToScalar();

                    double scalarResult = scalar1 + scalar2;
                    parameter1 = parameter1.WithElement(0, scalarResult);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter1.As<double, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
