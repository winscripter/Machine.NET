using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movaps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movaps_xmmm128_xmm:
                {
                    if (instruction.GetOpKind(0) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(instruction);
                        if (operand % 16 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
                        Memory.WriteBinaryVector128(operand, ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateXmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            case Code.Movaps_xmm_xmmm128:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong operand = GetMemOperand64(instruction);
                        if (operand % 16 != 0)
                        {
                            RaiseException(StaticErrors.GeneralProtectionFault);
                            break;
                        }
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            Memory.ReadBinaryVector128(operand));
                    }
                    else
                    {
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            ProcessorRegisters.EvaluateXmm(
                                instruction.GetOpRegister(1)
                            )
                        );
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
