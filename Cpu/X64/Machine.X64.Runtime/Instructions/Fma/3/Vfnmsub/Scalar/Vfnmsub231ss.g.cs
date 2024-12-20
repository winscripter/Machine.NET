// This file was auto-generated.
// See /eng/BuildTools/X64/FMA3Generator.

using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void Vfnmsub231ss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
                case Code.EVEX_Vfnmsub231ss_xmm_k1z_xmm_xmmm32_er:
                {
                    Vector128<float> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, float>();
                    Vector128<float> src2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, float>();

                    Vector128<float> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, float>();
                    result = result.WithElement(0, (result[0] * src2[0]) - src1[0]);

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
