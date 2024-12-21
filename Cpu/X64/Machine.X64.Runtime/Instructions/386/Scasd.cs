using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void scasd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Scasd_EAX_m32:
                {
                    uint temp = this.Memory.ReadUInt32((ulong)(this.ProcessorRegisters.Es << 4) + GetMemOperand32(in instruction));
                    ulong result = (ulong)((ulong)ProcessorRegisters.Eax - temp);
                    ProcessFlags(result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
