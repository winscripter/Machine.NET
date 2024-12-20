using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpcmpb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpcmpb_kr_k1_xmm_xmmm128_imm8:
                {
                    Vector128<sbyte> v1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, sbyte>();
                    Vector128<sbyte> v2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, sbyte>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector128<sbyte>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vpcmpb_kr_k1_ymm_ymmm256_imm8:
                {
                    Vector256<sbyte> v1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, sbyte>();
                    Vector256<sbyte> v2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, sbyte>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector256<sbyte>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vpcmpb_kr_k1_zmm_zmmm512_imm8:
                {
                    Vector512<sbyte> v1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, sbyte>();
                    Vector512<sbyte> v2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, sbyte>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector512<sbyte>.Count; i++)
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
