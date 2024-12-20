using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcmppd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcmppd_kr_k1_xmm_xmmm128b64_imm8:
                {
                    const double DoubleAllOnes = double.MaxValue;
                    const double DoubleZero = 0D;

                    Vector128<double> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> second = EvaluateXmmFromInstruction(in instruction, 2).As<float, double>();

                    switch ((byte)instruction.GetImmediate(3))
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
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(1), first.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcmppd_kr_k1_ymm_ymmm256b64_imm8:
                {
                    const double DoubleAllOnes = double.MaxValue;
                    const double DoubleZero = 0D;

                    Vector256<double> first = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector256<double> second = EvaluateYmmFromInstruction(in instruction, 2).As<float, double>();

                    switch ((byte)instruction.GetImmediate(3))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) == second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) == second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) == second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) < second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) < second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) < second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) <= second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) <= second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) <= second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, double.IsNaN(first.GetElement(0)) || double.IsNaN(second.GetElement(0)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, double.IsNaN(first.GetElement(1)) || double.IsNaN(second.GetElement(1)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, double.IsNaN(first.GetElement(2)) || double.IsNaN(second.GetElement(2)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, double.IsNaN(first.GetElement(3)) || double.IsNaN(second.GetElement(3)) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) > second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) > second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) > second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) >= second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) >= second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) >= second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !double.IsNaN(first.GetElement(0)) && !double.IsNaN(second.GetElement(0)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, !double.IsNaN(first.GetElement(1)) && !double.IsNaN(second.GetElement(1)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, !double.IsNaN(first.GetElement(2)) && !double.IsNaN(second.GetElement(2)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, !double.IsNaN(first.GetElement(3)) && !double.IsNaN(second.GetElement(3)) ? DoubleAllOnes : DoubleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(1), first.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcmppd_kr_k1_zmm_zmmm512b64_imm8_sae:
                {
                    const double DoubleAllOnes = double.MaxValue;
                    const double DoubleZero = 0D;

                    Vector512<double> first = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector512<double> second = EvaluateZmmFromInstruction(in instruction, 2).As<float, double>();

                    switch ((byte)instruction.GetImmediate(3))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) == second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) == second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) == second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, first.GetElement(4) == second.GetElement(4) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, first.GetElement(5) == second.GetElement(5) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, first.GetElement(6) == second.GetElement(6) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, first.GetElement(7) == second.GetElement(7) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) < second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) < second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) < second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, first.GetElement(4) < second.GetElement(4) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, first.GetElement(5) < second.GetElement(5) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, first.GetElement(6) < second.GetElement(6) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, first.GetElement(7) < second.GetElement(7) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) <= second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) <= second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) <= second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, first.GetElement(4) <= second.GetElement(4) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, first.GetElement(5) <= second.GetElement(5) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, first.GetElement(6) <= second.GetElement(6) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, first.GetElement(7) <= second.GetElement(7) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, double.IsNaN(first.GetElement(0)) || double.IsNaN(second.GetElement(0)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, double.IsNaN(first.GetElement(1)) || double.IsNaN(second.GetElement(1)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, double.IsNaN(first.GetElement(2)) || double.IsNaN(second.GetElement(2)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, double.IsNaN(first.GetElement(3)) || double.IsNaN(second.GetElement(3)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, double.IsNaN(first.GetElement(4)) || double.IsNaN(second.GetElement(4)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, double.IsNaN(first.GetElement(5)) || double.IsNaN(second.GetElement(5)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, double.IsNaN(first.GetElement(6)) || double.IsNaN(second.GetElement(6)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, double.IsNaN(first.GetElement(7)) || double.IsNaN(second.GetElement(7)) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) > second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) > second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) > second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, first.GetElement(4) > second.GetElement(4) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, first.GetElement(5) > second.GetElement(5) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, first.GetElement(6) > second.GetElement(6) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, first.GetElement(7) > second.GetElement(7) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= second.GetElement(0) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, first.GetElement(1) >= second.GetElement(1) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, first.GetElement(2) >= second.GetElement(2) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, first.GetElement(3) >= second.GetElement(3) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, first.GetElement(4) >= second.GetElement(4) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, first.GetElement(5) >= second.GetElement(5) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, first.GetElement(6) >= second.GetElement(6) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, first.GetElement(7) >= second.GetElement(7) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !double.IsNaN(first.GetElement(0)) && !double.IsNaN(second.GetElement(0)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(1, !double.IsNaN(first.GetElement(1)) && !double.IsNaN(second.GetElement(1)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(2, !double.IsNaN(first.GetElement(2)) && !double.IsNaN(second.GetElement(2)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(3, !double.IsNaN(first.GetElement(3)) && !double.IsNaN(second.GetElement(3)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(4, !double.IsNaN(first.GetElement(4)) && !double.IsNaN(second.GetElement(4)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(5, !double.IsNaN(first.GetElement(5)) && !double.IsNaN(second.GetElement(5)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(6, !double.IsNaN(first.GetElement(6)) && !double.IsNaN(second.GetElement(6)) ? DoubleAllOnes : DoubleZero);
                            first = first.WithElement(7, !double.IsNaN(first.GetElement(7)) && !double.IsNaN(second.GetElement(7)) ? DoubleAllOnes : DoubleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(1), first.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
