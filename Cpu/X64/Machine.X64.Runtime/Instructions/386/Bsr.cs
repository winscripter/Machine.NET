using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void bsr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Bsr_r16_rm16:
                {
                    ushort value = 0;
                    ushort rm16 = RMEvaluate16(in instruction, 1);
                    for (int i = 15; i >= 0; i--)
                    {
                        if (BitUtilities.IsBitSet(rm16, i))
                        {
                            break;
                        }
                        value++;
                    }
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), value);
                    break;
                }

            case Code.Bsr_r32_rm32:
                {
                    uint value = 0;
                    uint rm32 = RMEvaluate32(in instruction, 1);
                    for (int i = 31; i >= 0; i--)
                    {
                        if (BitUtilities.IsBitSet(rm32, i))
                        {
                            break;
                        }
                        value++;
                    }
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);
                    break;
                }

            case Code.Bsr_r64_rm64:
                {
                    ulong value = 0;
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    for (int i = 63; i >= 0; i--)
                    {
                        if (BitUtilities.IsBitSet(rm64, i))
                        {
                            break;
                        }
                        value++;
                    }
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
