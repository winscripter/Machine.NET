using Iced.Intel;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vp4dpwssd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vp4dpwssd_zmm_k1z_zmmp3_m128:
                {
                    Vector512<short> a = EvaluateZmmFromInstruction(in instruction, 1).As<float, short>();
                    Vector512<short> b = this.Memory.ReadBinaryVector512(GetMemOperand64(instruction)).As<float, short>();

                    int result = 0;
                    for (int i = 0; i < Vector512<short>.Count; i++)
                        result += a[i] * b[i];

                    AlterScalarOfXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
