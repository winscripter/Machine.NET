using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    // Examples:
    //   [ 1, 2, 3, 2] => [0, 1, 0, 1]
    //   [ 4, 3, 6, 4] => [1, 0, 0, 1]
    private void vpconflictq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpconflictq_xmm_k1z_xmmm128b64:
                {
                    Vector128<ulong> inputVector = EvaluateXmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector128<ulong> result = Vector128<ulong>.Zero;

                    for (int i = 0; i < Vector128<ulong>.Count; i++)
                    {
                        int itemCount = CountElements(inputVector, inputVector[i]);
                        result = result.WithElement(i, itemCount > 1 ? 1u : 0);
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;

                    static int CountElements(Vector128<ulong> vec, ulong items)
                    {
                        int count = 0;
                        for (int i = 0; i < Vector128<ulong>.Count; i++)
                        {
                            if (vec[i] == items)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                }

            case Code.EVEX_Vpconflictq_ymm_k1z_ymmm256b64:
                {
                    Vector256<ulong> inputVector = EvaluateYmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector256<ulong> result = Vector256<ulong>.Zero;

                    for (int i = 0; i < Vector256<ulong>.Count; i++)
                    {
                        int itemCount = CountElements(inputVector, inputVector[i]);
                        result = result.WithElement(i, itemCount > 1 ? 1u : 0);
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;

                    static int CountElements(Vector256<ulong> vec, ulong items)
                    {
                        int count = 0;
                        for (int i = 0; i < Vector256<ulong>.Count; i++)
                        {
                            if (vec[i] == items)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                }

            case Code.EVEX_Vpconflictq_zmm_k1z_zmmm512b64:
                {
                    Vector512<ulong> inputVector = EvaluateZmmFromInstruction(in instruction, 1).As<float, ulong>();
                    Vector512<ulong> result = Vector512<ulong>.Zero;

                    for (int i = 0; i < Vector512<ulong>.Count; i++)
                    {
                        int itemCount = CountElements(inputVector, inputVector[i]);
                        result = result.WithElement(i, itemCount > 1 ? 1u : 0);
                    }

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;

                    static int CountElements(Vector512<ulong> vec, ulong items)
                    {
                        int count = 0;
                        for (int i = 0; i < Vector512<ulong>.Count; i++)
                        {
                            if (vec[i] == items)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
