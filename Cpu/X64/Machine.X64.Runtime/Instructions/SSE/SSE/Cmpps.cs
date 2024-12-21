using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmpps(in Instruction instruction)
    {
        const float SingleAllOnes = float.MaxValue;
        const float SingleZero = 0F;

        switch (instruction.Code)
        {
            case Code.Cmpps_xmm_xmmm128_imm8:
                {
                    Vector128<float> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> second = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)),
                        _ => Vector128<float>.Zero
                    };

                    switch ((byte)instruction.GetImmediate(2))
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
                            first = first.WithElement(0, double.IsNaN(first.GetElement(0)) || double.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, double.IsNaN(first.GetElement(1)) || double.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
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
                            first = first.WithElement(0, !double.IsNaN(first.GetElement(0)) && !double.IsNaN(second.GetElement(0)) ? SingleAllOnes : SingleZero);
                            first = first.WithElement(1, !double.IsNaN(first.GetElement(1)) && !double.IsNaN(second.GetElement(1)) ? SingleAllOnes : SingleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), first);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
