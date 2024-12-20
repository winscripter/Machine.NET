using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvttsd2si(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvttsd2si_r32_xmmm64:
                {
                    double scalarValue = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand64(instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)scalarValue);
                    break;
                }

            case Code.Cvttsd2si_r64_xmmm64:
                {
                    double scalarValue = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand64(instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)scalarValue);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
