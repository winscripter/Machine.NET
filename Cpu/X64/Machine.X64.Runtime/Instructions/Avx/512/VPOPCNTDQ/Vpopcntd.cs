using Iced.Intel;
using Machine.X64.Component;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpopcntd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpopcntd_xmm_k1z_xmmm128b32:
                {
                    Vector128<uint> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> result = Vector128.Create(
                        (uint)BitOperations.PopCount(vec[0]),
                        (uint)BitOperations.PopCount(vec[1]),
                        (uint)BitOperations.PopCount(vec[2]),
                        (uint)BitOperations.PopCount(vec[3])
                    );
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpopcntd_ymm_k1z_ymmm256b32:
                {
                    Vector256<uint> vec = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> result = Vector256.Create(
                        (uint)BitOperations.PopCount(vec[0]),
                        (uint)BitOperations.PopCount(vec[1]),
                        (uint)BitOperations.PopCount(vec[2]),
                        (uint)BitOperations.PopCount(vec[3]),
                        (uint)BitOperations.PopCount(vec[4]),
                        (uint)BitOperations.PopCount(vec[5]),
                        (uint)BitOperations.PopCount(vec[6]),
                        (uint)BitOperations.PopCount(vec[7])
                    );
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpopcntd_zmm_k1z_zmmm512b32:
                {
                    Vector512<uint> vec = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> result = Vector512.Create(
                        (uint)BitOperations.PopCount(vec[0]),
                        (uint)BitOperations.PopCount(vec[1]),
                        (uint)BitOperations.PopCount(vec[2]),
                        (uint)BitOperations.PopCount(vec[3]),
                        (uint)BitOperations.PopCount(vec[4]),
                        (uint)BitOperations.PopCount(vec[5]),
                        (uint)BitOperations.PopCount(vec[6]),
                        (uint)BitOperations.PopCount(vec[7]),
                        (uint)BitOperations.PopCount(vec[8]),
                        (uint)BitOperations.PopCount(vec[9]),
                        (uint)BitOperations.PopCount(vec[10]),
                        (uint)BitOperations.PopCount(vec[11]),
                        (uint)BitOperations.PopCount(vec[12]),
                        (uint)BitOperations.PopCount(vec[13]),
                        (uint)BitOperations.PopCount(vec[14]),
                        (uint)BitOperations.PopCount(vec[15])
                    );
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
