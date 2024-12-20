using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vptestmq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vptestmq_kr_k1_xmm_xmmm128b64:
                {
                    Vector128<double> source = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> dest = EvaluateXmmFromInstruction(in instruction, 2).As<float, double>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, (BitConverter.DoubleToInt64Bits(source[i]) & BitConverter.DoubleToInt64Bits(dest[i])) == 0);
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestmq_kr_k1_ymm_ymmm256b64:
                {
                    Vector256<double> source = EvaluateYmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector256<double> dest = EvaluateYmmFromInstruction(in instruction, 2).As<float, double>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, (BitConverter.DoubleToInt64Bits(source[i]) & BitConverter.DoubleToInt64Bits(dest[i])) == 0);
                    }

                    ProcessorRegisters.SetK(register, k);
                    break;
                }

            case Code.EVEX_Vptestmq_kr_k1_zmm_zmmm512b64:
                {
                    Vector512<double> source = EvaluateZmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector512<double> dest = EvaluateZmmFromInstruction(in instruction, 2).As<float, double>();

                    Register register = instruction.GetOpRegister(0);
                    ulong k = ProcessorRegisters.EvaluateK(register);

                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        BitUtilities.SetBit(k, i, (BitConverter.DoubleToInt64Bits(source[i]) & BitConverter.DoubleToInt64Bits(dest[i])) == 0);
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
