using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvttpd2pi(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvttpd2pi_mm_xmmm128:
                {
                    Vector128<double> dpVector = instruction.GetOpKind(0) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand(in instruction)).As<float, double>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>(),
                        _ => Vector128<double>.Zero
                    };
                    ulong result = BitUtilities.CreateUInt64((uint)dpVector[1], (uint)dpVector[0]);
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
