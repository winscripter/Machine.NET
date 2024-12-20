using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void phsubw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Phsubw_mm_mmm64:
                {
                    Vector64<ushort> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, ushort>();
                    Vector64<ushort> dst = GetVectorFromMM(in instruction, 1).As<ulong, ushort>();

                    Vector64<ushort> result = Vector64.Create(
                        (ushort)(src[0] - src[1]),
                        (ushort)(src[2] - src[3]),
                        (ushort)(dst[0] - dst[1]),
                        (ushort)(dst[2] - dst[3])
                    );

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Phsubw_xmm_xmmm128:
                {
                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();

                    Vector128<ushort> result = Vector128.Create(
                        (ushort)(src[0] - src[1]),
                        (ushort)(src[2] - src[3]),
                        (ushort)(src[4] - src[5]),
                        (ushort)(src[6] - src[7]),
                        (ushort)(dst[0] - dst[1]),
                        (ushort)(dst[2] - dst[3]),
                        (ushort)(dst[4] - dst[5]),
                        (ushort)(dst[6] - dst[7])
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
