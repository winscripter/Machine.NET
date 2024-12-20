using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcmpsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcmpsd_kr_k1_xmm_xmmm64_imm8_sae:
                {
                    const double DoubleAllOnes = double.MaxValue;
                    const double DoubleZero = 0D;

                    Vector128<double> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    double scalar = ReadXmmScalarOrDouble(in instruction, 2);

                    switch ((byte)instruction.GetImmediate(3))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, double.IsNaN(first.GetElement(0)) || double.IsNaN(scalar) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !double.IsNaN(first.GetElement(0)) && !double.IsNaN(scalar) ? DoubleAllOnes : DoubleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(1), first.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
