using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void phsubd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Phsubd_mm_mmm64:
                {
                    Vector64<uint> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, uint>();
                    Vector64<uint> dst = GetVectorFromMM(in instruction, 1).As<ulong, uint>();

                    Vector64<uint> result = Vector64.Create(
                        (uint)(src[0] - src[1]),
                        (uint)(dst[0] - src[1])
                    );

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Phsubd_xmm_xmmm128:
                {
                    Vector128<uint> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();

                    Vector128<uint> result = Vector128.Create(
                        (uint)(src[0] - src[1]),
                        (uint)(src[2] - src[3]),
                        (uint)(dst[0] - dst[1]),
                        (uint)(dst[2] - dst[3])
                    );

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
