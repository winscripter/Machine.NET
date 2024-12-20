using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vptestnmw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vptestnmw_kr_k1_xmm_xmmm128:
                {
                    Vector128<ushort> source = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> dest = EvaluateXmmFromInstruction(in instruction, 2).As<float, ushort>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, !((BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0));
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestnmw_kr_k1_ymm_ymmm256:
                {
                    Vector256<ushort> source = EvaluateYmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector256<ushort> dest = EvaluateYmmFromInstruction(in instruction, 2).As<float, ushort>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector256<ushort>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, !((BitConverter.SingleToInt32Bits(source[i]) & BitConverter.SingleToInt32Bits(dest[i])) == 0));
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestnmw_kr_k1_zmm_zmmm512:
                {
                    Vector512<ushort> source = EvaluateZmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector512<ushort> dest = EvaluateZmmFromInstruction(in instruction, 2).As<float, ushort>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector512<ushort>.Count; i++)
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
