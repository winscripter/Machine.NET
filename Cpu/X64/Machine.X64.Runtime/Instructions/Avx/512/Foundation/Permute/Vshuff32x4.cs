using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vshuff32x4(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vshuff32x4_ymm_k1z_ymm_ymmm256b32_imm8:
                {
                    Register ymmDestination = instruction.GetOpRegister(0);
                    Vector256<float> source1 = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> source2 = EvaluateYmmFromInstruction(in instruction, 2);
                    byte controlMask = (byte)instruction.GetImmediate(1);

                    ReadOnlySpan<VectorShufflingMode> shufflingModes = Shuffle.ReadShufflingModes(controlMask);
                    Vector256<float> result = Shuffle.Vectors(source1, source2, shufflingModes);

                    ProcessorRegisters.SetYmm(ymmDestination, result);
                    break;
                }

            case Code.EVEX_Vshuff32x4_zmm_k1z_zmm_zmmm512b32_imm8:
                {
                    Register ymmDestination = instruction.GetOpRegister(0);
                    Vector512<float> source1 = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> source2 = EvaluateZmmFromInstruction(in instruction, 2);
                    byte controlMask = (byte)instruction.GetImmediate(1);

                    ReadOnlySpan<VectorShufflingMode> shufflingModes = Shuffle.ReadShufflingModes(controlMask);
                    Vector512<float> result = Shuffle.Vectors(source1, source2, shufflingModes);

                    ProcessorRegisters.SetZmm(ymmDestination, result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
