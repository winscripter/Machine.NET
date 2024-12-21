using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmppd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmppd_xmm_xmmm128_imm8:
                {
                    const double DoubleAllOnes = double.MaxValue;
                    const double DoubleZero = 0D;

                    Vector128<double> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    Vector128<double> second = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction)).As<float, double>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>(),
                        _ => Vector128<double>.Zero
                    };

                    switch ((byte)instruction.GetImmediate(2))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) == second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) < second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) <= second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, double.IsNaN(first.GetElement(0)) || double.IsNaN(second.GetElement(0)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, double.IsNaN(first.GetElement(1)) || double.IsNaN(second.GetElement(1)) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) > second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) >= second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !double.IsNaN(first.GetElement(0)) && !double.IsNaN(second.GetElement(0)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, !double.IsNaN(first.GetElement(1)) && !double.IsNaN(second.GetElement(1)) ? DoubleAllOnes : DoubleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), first.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
