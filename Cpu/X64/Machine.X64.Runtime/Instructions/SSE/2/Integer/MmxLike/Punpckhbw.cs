using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void punpckhbw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Punpckhbw_mm_mmm64:
                {
                    WriteVector64ToMM(
                        in instruction,
                        0,
                        VectorUnpackInterleave.UnpackInterleaveHighOrder(
                            GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, byte>(),
                            GetVectorFromMM(in instruction, 0).As<ulong, byte>()
                        )
                    );
                    break;
                }

            case Code.Punpckhbw_xmm_xmmm128:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        VectorUnpackInterleave.UnpackInterleaveHighOrder(
                            this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, byte>(),
                            this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>()
                        )
                        .As<byte, float>()
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
