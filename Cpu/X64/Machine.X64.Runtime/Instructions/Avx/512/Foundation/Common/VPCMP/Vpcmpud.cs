using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;
public partial class CpuRuntime
{
    private void vpcmpud(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpcmpud_kr_k1_xmm_xmmm128b32_imm8:
                {
                    Vector128<uint> v1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector128<uint> v2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, uint>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector128<uint>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vpcmpud_kr_k1_ymm_ymmm256b32_imm8:
                {
                    Vector256<uint> v1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector256<uint> v2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, uint>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector256<uint>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vpcmpud_kr_k1_zmm_zmmm512b32_imm8:
                {
                    Vector512<uint> v1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, uint>();
                    Vector512<uint> v2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, uint>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector512<uint>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
