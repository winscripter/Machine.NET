using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpopcntq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpopcntq_xmm_k1z_xmmm128b64:
                {
                    Vector128<ulong> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> result = Vector128.Create(
                        (ulong)BitUtilities.PopCount(vec[0]),
                        (ulong)BitUtilities.PopCount(vec[1])
                    );
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpopcntq_ymm_k1z_ymmm256b64:
                {
                    Vector256<ulong> vec = EvaluateYmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector256<ulong> result = Vector256.Create(
                        (ulong)BitUtilities.PopCount(vec[0]),
                        (ulong)BitUtilities.PopCount(vec[1]),
                        (ulong)BitUtilities.PopCount(vec[2]),
                        (ulong)BitUtilities.PopCount(vec[3])
                    );
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpopcntq_zmm_k1z_zmmm512b64:
                {
                    Vector512<ulong> vec = EvaluateZmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector512<ulong> result = Vector512.Create(
                        (ulong)BitUtilities.PopCount(vec[0]),
                        (ulong)BitUtilities.PopCount(vec[1]),
                        (ulong)BitUtilities.PopCount(vec[2]),
                        (ulong)BitUtilities.PopCount(vec[3]),
                        (ulong)BitUtilities.PopCount(vec[4]),
                        (ulong)BitUtilities.PopCount(vec[5]),
                        (ulong)BitUtilities.PopCount(vec[6]),
                        (ulong)BitUtilities.PopCount(vec[7])
                    );
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
