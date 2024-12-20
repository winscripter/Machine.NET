using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void bt(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Bt_rm16_imm8:
                {
                    ushort rm16 = (ushort)instruction.GetImmediate(0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    bool bit = BitUtilities.IsBitSet(rm16, imm8);

                    this.ProcessorRegisters.RFlagsCF = bit;
                    break;
                }

            case Code.Bt_rm16_r16:
                {
                    ushort rm16 = (ushort)instruction.GetImmediate(0);
                    ushort r16 = this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1));

                    bool bit = BitUtilities.IsBitSet(rm16, r16);

                    this.ProcessorRegisters.RFlagsCF = bit;
                    break;
                }

            case Code.Bt_rm32_imm8:
                {
                    uint rm32 = (uint)instruction.GetImmediate(0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    bool bit = BitUtilities.IsBitSet(rm32, imm8);

                    this.ProcessorRegisters.RFlagsCF = bit;
                    break;
                }

            case Code.Bt_rm32_r32:
                {
                    uint rm32 = (uint)instruction.GetImmediate(0);
                    uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));

                    bool bit = BitUtilities.IsBitSet(rm32, r32);

                    this.ProcessorRegisters.RFlagsCF = bit;
                    break;
                }

            case Code.Bt_rm64_imm8:
                {
                    ulong rm64 = (ulong)instruction.GetImmediate(0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    bool bit = BitUtilities.IsBitSet(rm64, imm8);

                    this.ProcessorRegisters.RFlagsCF = bit;
                    break;
                }

            case Code.Bt_rm64_r64:
                {
                    ulong rm64 = (ulong)instruction.GetImmediate(0);
                    byte imm8 = (byte)instruction.GetImmediate(1);

                    bool bit = BitUtilities.IsBitSet(rm64, imm8);

                    this.ProcessorRegisters.RFlagsCF = bit;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
