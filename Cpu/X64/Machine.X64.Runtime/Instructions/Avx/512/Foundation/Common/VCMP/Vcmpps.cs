using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcmpps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcmpps_kr_k1_xmm_xmmm128b32_imm8:
                {
                    const float SingleAllOnes = float.MaxValue;
                    const float SingleZero = 0F;

                    Vector128<float> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> second = EvaluateXmmFromInstruction(in instruction, 2);

                    switch ((byte)instruction.GetImmediate(3))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) == second.GetElement(1) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) < second.GetElement(1) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) <= second.GetElement(1) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, float.IsNaN(first.GetElement(0)) || float.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, float.IsNaN(first.GetElement(1)) || float.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) > second.GetElement(1) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) >= second.GetElement(1) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !float.IsNaN(first.GetElement(0)) && !float.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, !float.IsNaN(first.GetElement(1)) && !float.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(1), first);
                    break;
                }

            case Code.EVEX_Vcmpps_kr_k1_ymm_ymmm256b32_imm8:
                {
                    const float SingleAllOnes = float.MaxValue;
                    const float SingleZero = 0F;

                    Vector256<float> first = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    Vector256<float> second = EvaluateYmmFromInstruction(in instruction, 2);

                    switch ((byte)instruction.GetImmediate(3))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) == second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) == second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) == second.GetElement(3) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) < second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) < second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) < second.GetElement(3) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) <= second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) <= second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) <= second.GetElement(3) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, float.IsNaN(first.GetElement(0)) || float.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, float.IsNaN(first.GetElement(1)) || float.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, float.IsNaN(first.GetElement(2)) || float.IsNaN(second.GetElement(2)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, float.IsNaN(first.GetElement(3)) || float.IsNaN(second.GetElement(3)) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) > second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) > second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) > second.GetElement(3) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) >= second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) >= second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) >= second.GetElement(3) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !float.IsNaN(first.GetElement(0)) && !float.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, !float.IsNaN(first.GetElement(1)) && !float.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, !float.IsNaN(first.GetElement(2)) && !float.IsNaN(second.GetElement(2)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, !float.IsNaN(first.GetElement(3)) && !float.IsNaN(second.GetElement(3)) ? SingleAllOnes : SingleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(1), first);
                    break;
                }

            case Code.EVEX_Vcmpps_kr_k1_zmm_zmmm512b32_imm8_sae:
                {
                    const float SingleAllOnes = float.MaxValue;
                    const float SingleZero = 0F;

                    Vector512<float> first = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1));
                    Vector512<float> second = EvaluateZmmFromInstruction(in instruction, 2);

                    switch ((byte)instruction.GetImmediate(3))
                    {
                        case CmppsModes.CmppsEqual:
                            first = first.WithElement(0, first.GetElement(0) == second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) == second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) == second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) == second.GetElement(3) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, first.GetElement(4) == second.GetElement(4) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, first.GetElement(5) == second.GetElement(5) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, first.GetElement(6) == second.GetElement(6) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, first.GetElement(7) == second.GetElement(7) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsLessThan:
                            first = first.WithElement(0, first.GetElement(0) < second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) < second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) < second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) < second.GetElement(3) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, first.GetElement(4) < second.GetElement(4) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, first.GetElement(5) < second.GetElement(5) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, first.GetElement(6) < second.GetElement(6) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, first.GetElement(7) < second.GetElement(7) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) <= second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) <= second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) <= second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) <= second.GetElement(3) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, first.GetElement(4) <= second.GetElement(4) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, first.GetElement(5) <= second.GetElement(5) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, first.GetElement(6) <= second.GetElement(6) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, first.GetElement(7) <= second.GetElement(7) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsUnordered:
                            first = first.WithElement(0, float.IsNaN(first.GetElement(0)) || float.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, float.IsNaN(first.GetElement(1)) || float.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, float.IsNaN(first.GetElement(2)) || float.IsNaN(second.GetElement(2)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, float.IsNaN(first.GetElement(3)) || float.IsNaN(second.GetElement(3)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, float.IsNaN(first.GetElement(4)) || float.IsNaN(second.GetElement(4)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, float.IsNaN(first.GetElement(5)) || float.IsNaN(second.GetElement(5)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, float.IsNaN(first.GetElement(6)) || float.IsNaN(second.GetElement(6)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, float.IsNaN(first.GetElement(7)) || float.IsNaN(second.GetElement(7)) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) > second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) > second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) > second.GetElement(3) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, first.GetElement(4) > second.GetElement(4) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, first.GetElement(5) > second.GetElement(5) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, first.GetElement(6) > second.GetElement(6) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, first.GetElement(7) > second.GetElement(7) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= second.GetElement(0) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, first.GetElement(1) >= second.GetElement(1) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, first.GetElement(2) >= second.GetElement(2) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, first.GetElement(3) >= second.GetElement(3) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, first.GetElement(4) >= second.GetElement(4) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, first.GetElement(5) >= second.GetElement(5) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, first.GetElement(6) >= second.GetElement(6) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, first.GetElement(7) >= second.GetElement(7) ? SingleAllOnes : SingleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !float.IsNaN(first.GetElement(0)) && !float.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, !float.IsNaN(first.GetElement(1)) && !float.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(2, !float.IsNaN(first.GetElement(2)) && !float.IsNaN(second.GetElement(2)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(3, !float.IsNaN(first.GetElement(3)) && !float.IsNaN(second.GetElement(3)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(4, !float.IsNaN(first.GetElement(4)) && !float.IsNaN(second.GetElement(4)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(5, !float.IsNaN(first.GetElement(5)) && !float.IsNaN(second.GetElement(5)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(6, !float.IsNaN(first.GetElement(6)) && !float.IsNaN(second.GetElement(6)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(7, !float.IsNaN(first.GetElement(7)) && !float.IsNaN(second.GetElement(7)) ? SingleAllOnes : SingleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(1), first);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
