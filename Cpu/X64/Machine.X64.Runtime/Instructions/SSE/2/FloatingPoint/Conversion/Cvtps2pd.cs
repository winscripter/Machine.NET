using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtps2pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtps2pd_xmm_xmmm64:
                {
                    (float a, float b) = (0F, 0F);
                    OpKind kind = instruction.GetOpKind(1);

                    switch (kind)
                    {
                        case OpKind.Memory:
                            ulong memOperand = GetMemOperand64(in instruction);
                            (a, b) = (this.Memory.ReadSingle(memOperand), this.Memory.ReadSingle(memOperand + 4));
                            break;

                        case OpKind.Register:
                            Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                            (a, b) = (xmm[0], xmm[1]);
                            break;
                    }

                    Vector128<double> result = Vector128.Create((double)a, (double)b);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
