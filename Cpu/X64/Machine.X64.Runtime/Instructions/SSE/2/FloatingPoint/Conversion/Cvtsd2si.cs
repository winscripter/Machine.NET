using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtsd2si(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtsd2si_r32_xmmm64:
                {
                    double scalar = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)scalar);
                    break;
                }

            case Code.Cvtsd2si_r64_xmmm64:
                {
                    double scalar = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)scalar);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
