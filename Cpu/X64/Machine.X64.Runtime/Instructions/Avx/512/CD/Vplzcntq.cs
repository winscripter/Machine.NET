using Iced.Intel;
using Machine.X64.Component;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vplzcntq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vplzcntq_xmm_k1z_xmmm128b64:
                {
                    Vector128<ulong> inputVector = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> result = Vector128<ulong>.Zero;

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            (ulong)BitOperations.LeadingZeroCount(inputVector[0]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vplzcntq_ymm_k1z_ymmm256b64:
                {
                    Vector256<ulong> inputVector = EvaluateYmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector256<ulong> result = Vector256<ulong>.Zero;

                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            (ulong)BitOperations.LeadingZeroCount(inputVector[0]));
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vplzcntq_zmm_k1z_zmmm512b64:
                {
                    Vector512<ulong> inputVector = EvaluateZmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector512<ulong> result = Vector512<ulong>.Zero;

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            (ulong)BitOperations.LeadingZeroCount(inputVector[0]));
                    }

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
