using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void btc(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Btc_rm16_imm8:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    ushort complemented = BitUtilities.Complement(rm16, imm8);
                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm16, imm8);

                    RMSet16(in instruction, complemented);

                    break;
                }

            case Code.Btc_rm16_r16:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 0);
                    ushort r16 = this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1));

                    ushort complemented = BitUtilities.Complement(rm16, r16);
                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm16, r16);

                    RMSet16(in instruction, complemented);

                    break;
                }

            case Code.Btc_rm32_imm8:
                {
                    uint rm32 = RMEvaluate32(in instruction, 0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    uint complemented = BitUtilities.Complement(rm32, imm8);
                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm32, imm8);

                    RMSet32(in instruction, complemented);

                    break;
                }

            case Code.Btc_rm32_r32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 0);
                    uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));

                    uint complemented = BitUtilities.Complement(rm32, r32);
                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm32, r32);

                    RMSet32(in instruction, complemented);

                    break;
                }

            case Code.Btc_rm64_imm8:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    ulong complemented = BitUtilities.Complement(rm64, imm8);
                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm64, imm8);

                    RMSet64(in instruction, complemented);

                    break;
                }

            case Code.Btc_rm64_r64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 0);
                    ulong r64 = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));

                    ulong complemented = BitUtilities.Complement(rm64, r64);
                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm64, r64);

                    RMSet64(in instruction, complemented);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
