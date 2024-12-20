using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpcmpw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpcmpw_kr_k1_xmm_xmmm128_imm8:
                {
                    Vector128<short> v1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, short>();
                    Vector128<short> v2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, short>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vpcmpw_kr_k1_ymm_ymmm256_imm8:
                {
                    Vector256<short> v1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, short>();
                    Vector256<short> v2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, short>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector256<short>.Count; i++)
                    {
                        VpcmpComparer.Compare(ref result, i, v1[i], v2[i], mode);
                    }
                    ProcessorRegisters.SetK(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Vpcmpw_kr_k1_zmm_zmmm512_imm8:
                {
                    Vector512<short> v1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, short>();
                    Vector512<short> v2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, short>();
                    ulong result = ProcessorRegisters.EvaluateK(instruction.GetOpRegister(0));
                    VpcmpMode mode = (VpcmpMode)(byte)instruction.GetImmediate(3);
                    for (int i = 0; i < Vector512<short>.Count; i++)
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
