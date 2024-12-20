using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vrcp28pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vrcp28pd_zmm_k1z_zmmm512b64_sae:
                {
                    Vector512<double> v512 = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<double> result = Vector512<double>.Zero;

                    for (int i = 0; i < Vector512<double>.Count; i++)
                        result = result.WithElement(i, Math.ReciprocalEstimate(v512[i]));

                    result = result.K1z(0D, this.ProcessorRegisters.K1);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
