using Iced.Intel;
using Machine.Utility;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmpxchg8b(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmpxchg8b_m64:
                {
                    ulong m64 = this.Memory.ReadUInt64(GetMemOperand64(in instruction));

                    if (BitUtilities.CreateUInt64(this.ProcessorRegisters.Edx, this.ProcessorRegisters.Eax) == m64)
                    {
                        this.ProcessorRegisters.RFlagsZF = true;
                        ulong writebackM64 = BitUtilities.CreateUInt64(this.ProcessorRegisters.Ecx, this.ProcessorRegisters.Ebx);
                        this.Memory.WriteUInt64(GetMemOperand64(in instruction), writebackM64);
                    }
                    else
                    {
                        uint edx = BitUtilities.GetUpper32Bits(m64);
                        uint eax = BitUtilities.GetLower32Bits(m64);
                        this.ProcessorRegisters.Edx = edx;
                        this.ProcessorRegisters.Eax = eax;
                        this.ProcessorRegisters.RFlagsZF = false;
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
