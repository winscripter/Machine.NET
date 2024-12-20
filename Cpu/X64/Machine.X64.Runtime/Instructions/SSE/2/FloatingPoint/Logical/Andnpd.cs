using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void andnpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Andnpd_xmm_xmmm128:
                {
                    Register destination = instruction.GetOpRegister(0);
                    Vector128<double> dest = ProcessorRegisters.EvaluateXmm(destination).As<float, double>();
                    Vector128<double> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> result = Vector128<double>.Zero;

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        ulong destU64 = BitConverter.DoubleToUInt64Bits(dest[i]);
                        ulong srcU64 = BitConverter.DoubleToUInt64Bits(src[i]);

                        result = result.WithElement(i, destU64 & ~srcU64);
                    }

                    ProcessorRegisters.SetXmm(destination, result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
