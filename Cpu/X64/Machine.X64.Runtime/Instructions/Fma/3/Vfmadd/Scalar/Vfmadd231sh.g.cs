// This file was auto-generated.
// See /eng/BuildTools/X64/FMA3Generator.

using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void Vfmadd231sh(in Instruction instruction)
    {
        switch (instruction.Code)
        {
                case Code.EVEX_Vfmadd231sh_xmm_k1z_xmm_xmmm16_er:
                {
                    Vector128<Half> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, Half>();
                    Vector128<Half> src2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, Half>();

                    Vector128<Half> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, Half>();
                    result = result.WithElement(0, (result[0] * src2[0]) + src1[0]);

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<Half, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
