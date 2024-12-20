using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void roundsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Roundsd_xmm_xmmm64_imm8:
                {
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Memory:
                            AlterScalarOfXmm(
                                instruction.GetOpRegister(0),
                                Round.Double(
                                    this.Memory.ReadDouble(GetMemOperand64(instruction)),
                                    (byte)instruction.GetImmediate(2)
                                )
                            );
                            break;

                        case OpKind.Register:
                            this.ProcessorRegisters.SetXmm(
                                instruction.GetOpRegister(1),
                                Round.ScalarDouble128(
                                    this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>(),
                                    (byte)instruction.GetImmediate(2)
                                ).As<double, float>()
                            );
                            break;
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
