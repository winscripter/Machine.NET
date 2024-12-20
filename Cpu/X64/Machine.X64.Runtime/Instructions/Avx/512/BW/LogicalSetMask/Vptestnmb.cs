using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vptestnmb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vptestnmb_kr_k1_xmm_xmmm128:
                {
                    Vector128<byte> source = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> dest = EvaluateXmmFromInstruction(in instruction, 2).As<float, byte>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector128<byte>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, !((BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0));
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestnmb_kr_k1_ymm_ymmm256:
                {
                    Vector256<byte> source = EvaluateYmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector256<byte> dest = EvaluateYmmFromInstruction(in instruction, 2).As<float, byte>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector256<byte>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, !((BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0));
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestnmb_kr_k1_zmm_zmmm512:
                {
                    Vector512<byte> source = EvaluateZmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector512<byte> dest = EvaluateZmmFromInstruction(in instruction, 2).As<float, byte>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector512<byte>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, !((BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0));
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
