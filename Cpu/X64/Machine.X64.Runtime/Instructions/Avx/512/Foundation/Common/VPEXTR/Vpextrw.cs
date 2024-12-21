using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpextrw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpextrw_r32m16_xmm_imm8:
                {
                    byte index = (byte)instruction.GetImmediate(2);
                    if (index > 8)
                    {
                        break;
                    }

                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 1).AsUInt16();
                    switch (instruction.GetOpKind(0))
                    {
                        case OpKind.Register:
                            {
                                uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));
                                BitUtilities.SetLower16Bits(ref r32, src[index]);
                                this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), r32);
                                break;
                            }

                        case OpKind.Memory:
                            {
                                this.Memory.WriteUInt16(GetMemOperand16(in instruction), src[index]);
                                break;
                            }
                    }

                    break;
                }

            case Code.EVEX_Vpextrw_r32_xmm_imm8:
                {
                    byte index = (byte)instruction.GetImmediate(2);
                    if (index > 8)
                    {
                        break;
                    }

                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 1).AsUInt16();
                    uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));
                    BitUtilities.SetLower16Bits(ref r32, src[index]);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), r32);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
