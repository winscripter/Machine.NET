using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Sse;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void roundss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Roundss_xmm_xmmm32_imm8:
                {
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Memory:
                            AlterScalarOfXmm(
                                instruction.GetOpRegister(0),
                                Round.Single(
                                    this.Memory.ReadSingle(GetMemOperand(in instruction)),
                                    (byte)instruction.GetImmediate(2)
                                )
                            );
                            break;

                        case OpKind.Register:
                            this.ProcessorRegisters.SetXmm(
                                instruction.GetOpRegister(1),
                                Round.ScalarSingle128(
                                    this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)),
                                    (byte)instruction.GetImmediate(2)
                                )
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
