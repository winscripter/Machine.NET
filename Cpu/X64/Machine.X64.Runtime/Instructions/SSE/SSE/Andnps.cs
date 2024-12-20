using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void andnps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Andnps_xmm_xmmm128:
                {
                    Register destination = instruction.GetOpRegister(0);
                    Vector128<float> dest = ProcessorRegisters.EvaluateXmm(destination);
                    Vector128<float> src = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = Vector128<float>.Zero;

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        ulong destU64 = BitConverter.DoubleToUInt64Bits(dest[i]);
                        ulong srcU64 = BitConverter.DoubleToUInt64Bits(src[i]);

                        result = result.WithElement(i, destU64 & ~srcU64);
                    }

                    ProcessorRegisters.SetXmm(destination, result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
