using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrcp28sd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrcp28sd_xmm_k1z_xmm_xmmm64_sae:
                {
                    Vector128<double> v128 = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    double scalar = instruction.GetOpKind(2) switch
                    {
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, double>().ToScalar(),
                        OpKind.Memory => this.Memory.ReadUInt64(GetMemOperand64(instruction)),
                        _ => 0D
                    };

                    v128 = v128.WithElement(0, Math.ReciprocalEstimate(scalar));
                    v128 = v128.K1z(0D, this.ProcessorRegisters.K1);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), v128.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
