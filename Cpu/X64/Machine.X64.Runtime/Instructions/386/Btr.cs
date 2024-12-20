using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void btr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Btr_rm16_imm8:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm16, imm8);
                    ushort altered = BitUtilities.Zero(rm16, imm8);

                    RMSet16(in instruction, altered);

                    break;
                }

            case Code.Btr_rm16_r16:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 0);
                    ushort r16 = this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1));

                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm16, r16);
                    ushort altered = BitUtilities.Zero(rm16, r16);

                    RMSet16(in instruction, altered);

                    break;
                }

            case Code.Btr_rm32_imm8:
                {
                    uint rm32 = RMEvaluate32(in instruction, 0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm32, imm8);
                    ushort altered = BitUtilities.Zero(rm32, imm8);

                    RMSet32(in instruction, altered);

                    break;
                }

            case Code.Btr_rm32_r32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 0);
                    uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));

                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm32, r32);
                    ushort altered = BitUtilities.Zero(rm32, r32);

                    RMSet32(in instruction, altered);

                    break;
                }

            case Code.Btr_rm64_imm8:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm64, imm8);
                    ushort altered = BitUtilities.Zero(rm64, imm8);

                    RMSet64(in instruction, altered);

                    break;
                }

            case Code.Btr_rm64_r64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 0);
                    ulong r64 = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));

                    this.ProcessorRegisters.RFlagsCF = BitUtilities.IsBitSet(rm64, r64);
                    ushort altered = BitUtilities.Zero(rm64, r64);

                    RMSet64(in instruction, altered);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
