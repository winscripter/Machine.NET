using Iced.Intel;
using Machine.X64.Component;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vplzcntd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vplzcntd_xmm_k1z_xmmm128b32:
                {
                    Vector128<uint> inputVector = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> result = Vector128<uint>.Zero;

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            (uint)BitOperations.LeadingZeroCount(inputVector[0]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vplzcntd_ymm_k1z_ymmm256b32:
                {
                    Vector256<uint> inputVector = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> result = Vector256<uint>.Zero;

                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            (uint)BitOperations.LeadingZeroCount(inputVector[0]));
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vplzcntd_zmm_k1z_zmmm512b32:
                {
                    Vector512<uint> inputVector = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> result = Vector512<uint>.Zero;

                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            (uint)BitOperations.LeadingZeroCount(inputVector[0]));
                    }

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
