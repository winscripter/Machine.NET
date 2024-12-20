using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    // Examples:
    //   [ 1, 2, 3, 2] => [0, 1, 0, 1]
    //   [ 4, 3, 6, 4] => [1, 0, 0, 1]
    private void vpconflictd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpconflictd_xmm_k1z_xmmm128b32:
                {
                    Vector128<uint> inputVector = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> result = Vector128<uint>.Zero;

                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        int itemCount = CountElements(inputVector, inputVector[i]);
                        result = result.WithElement(i, itemCount > 1 ? 1u : 0);
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;

                    static int CountElements(Vector128<uint> vec, uint items)
                    {
                        int count = 0;
                        for (int i = 0; i< Vector128<uint>.Count; i++)
                        {
                            if (vec[i] == items)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                }

            case Code.EVEX_Vpconflictd_ymm_k1z_ymmm256b32:
                {
                    Vector256<uint> inputVector = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> result = Vector256<uint>.Zero;

                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        int itemCount = CountElements(inputVector, inputVector[i]);
                        result = result.WithElement(i, itemCount > 1 ? 1u : 0);
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;

                    static int CountElements(Vector256<uint> vec, uint items)
                    {
                        int count = 0;
                        for (int i = 0; i < Vector256<uint>.Count; i++)
                        {
                            if (vec[i] == items)
                            {
                                count++;
                            }
                        }
                        return count;
                    }
                }

            case Code.EVEX_Vpconflictd_zmm_k1z_zmmm512b32:
                {
                    Vector512<uint> inputVector = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> result = Vector512<uint>.Zero;

                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        int itemCount = CountElements(inputVector, inputVector[i]);
                        result = result.WithElement(i, itemCount > 1 ? 1u : 0);
                    }

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;

                    static int CountElements(Vector512<uint> vec, uint items)
                    {
                        int count = 0;
                        for (int i = 0; i < Vector512<uint>.Count; i++)
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
