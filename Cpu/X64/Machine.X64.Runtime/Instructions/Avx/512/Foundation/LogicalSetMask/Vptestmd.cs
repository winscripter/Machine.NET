using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vptestmd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vptestmd_kr_k1_xmm_xmmm128b32:
                {
                    Vector128<float> source = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> dest = EvaluateXmmFromInstruction(in instruction, 2);

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, (BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0);
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestmd_kr_k1_ymm_ymmm256b32:
                {
                    Vector256<float> source = EvaluateYmmFromInstruction(in instruction, 1);
                    Vector256<float> dest = EvaluateYmmFromInstruction(in instruction, 2);

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, (BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0);
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestmd_kr_k1_zmm_zmmm512b32:
                {
                    Vector512<float> source = EvaluateZmmFromInstruction(in instruction, 1);
                    Vector512<float> dest = EvaluateZmmFromInstruction(in instruction, 2);

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector512<float>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, (BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0);
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
