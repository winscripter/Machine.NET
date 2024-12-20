using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void extractps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Extractps_r64m32_xmm_imm8:
                {
                    byte immediate = (byte)instruction.GetImmediate(2);
                    if (immediate > 7)
                    {
                        RaiseException(StaticErrors.GeneralProtectionFault);
                    }

                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    OpKind opKind = instruction.GetOpKind(0);
                    switch (opKind)
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.WriteToRegister64(
                                instruction.GetOpRegister(0),
                                BitConverter.SingleToUInt32Bits(xmm[immediate])
                            );
                            break;

                        case OpKind.Memory:
                            this.Memory.WriteUInt32(
                                GetMemOperand64(instruction),
                                BitConverter.SingleToUInt32Bits(xmm[immediate])
                            );
                            break;
                    }

                    break;
                }

            case Code.Extractps_rm32_xmm_imm8:
                {
                    byte immediate = (byte)instruction.GetImmediate(2);
                    if (immediate > 7)
                    {
                        RaiseException(StaticErrors.GeneralProtectionFault);
                    }

                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    RMSet32(in instruction, BitConverter.SingleToUInt32Bits(xmm[immediate]), 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
