using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcmpss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcmpss_kr_k1_xmm_xmmm32_imm8_sae:
                {
                    const float DoubleAllOnes = float.MaxValue;
                    const float DoubleZero = 0F;

                    Vector128<float> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, float>();
                    float scalar = ReadXmmScalarOrSingle(in instruction, 2);

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
                            first = first.WithElement(0, float.IsNaN(first.GetElement(0)) || float.IsNaN(scalar) ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThan:
                            first = first.WithElement(0, first.GetElement(0) > scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsNotLessThanOrEqual:
                            first = first.WithElement(0, first.GetElement(0) >= scalar ? DoubleAllOnes : DoubleZero);
                            break;

                        case CmppsModes.CmppsOrdered:
                            first = first.WithElement(0, !float.IsNaN(first.GetElement(0)) && !float.IsNaN(scalar) ? DoubleAllOnes : DoubleZero);
                            break;
                    };
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(1), first.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
