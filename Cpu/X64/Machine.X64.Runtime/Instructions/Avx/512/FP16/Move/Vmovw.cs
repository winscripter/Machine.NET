using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovw_r32m16_xmm:
                {
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            {
                                this.ProcessorRegisters.WriteToRegister32(
                                    instruction.GetOpRegister(0),
                                    (uint)this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsHalf().ToScalar()
                                );
                                break;
                            }

                        case OpKind.Memory:
                            {
                                Half scalar = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsHalf().ToScalar();
                                this.Memory.WriteHalf(GetMemOperand64(instruction), scalar);
                                break;
                            }
                    }
                    break;
                }

            case Code.EVEX_Vmovw_r64m16_xmm:
                {
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            {
                                this.ProcessorRegisters.WriteToRegister64(
                                    instruction.GetOpRegister(0),
                                    (uint)this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsHalf().ToScalar()
                                );
                                break;
                            }

                        case OpKind.Memory:
                            {
                                Half scalar = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsHalf().ToScalar();
                                this.Memory.WriteHalf(GetMemOperand64(instruction), scalar);
                                break;
                            }
                    }
                    break;
                }

            case Code.EVEX_Vmovw_xmm_r32m16:
                {
                    switch (instruction.GetOpKind(1))
                    {
                        case OpKind.Register:
                            {
                                AlterScalarOfXmm(
                                    instruction.GetOpRegister(0),
                                    (Half)this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1))
                                );
                                break;
                            }

                        case OpKind.Memory:
                            {
                                AlterScalarOfXmm(
                                    instruction.GetOpRegister(0),
                                    this.Memory.ReadHalf(GetMemOperand64(instruction))
                                );
                                break;
                            }
                    }
                    break;
                }

            case Code.EVEX_Vmovw_xmm_r64m16:
                {
                    switch (instruction.GetOpKind(1))
                    {
                        case OpKind.Register:
                            {
                                AlterScalarOfXmm(
                                    instruction.GetOpRegister(0),
                                    (Half)this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1))
                                );
                                break;
                            }

                        case OpKind.Memory:
                            {
                                AlterScalarOfXmm(
                                    instruction.GetOpRegister(0),
                                    this.Memory.ReadHalf(GetMemOperand64(instruction))
                                );
                                break;
                            }
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
