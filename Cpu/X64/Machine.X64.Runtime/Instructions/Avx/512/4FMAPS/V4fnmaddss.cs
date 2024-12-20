using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void v4fnmaddss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_V4fnmaddss_xmm_k1z_xmmp3_m128:
                {
                    Vector512<float> a = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));
                    Vector512<float> b = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> c = this.Memory.ReadBinaryVector512(GetMemOperand64(instruction));

                    Vector512<float> result = a.WithElement(0, (a.ToScalar() * b.ToScalar()) - c.ToScalar());
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
