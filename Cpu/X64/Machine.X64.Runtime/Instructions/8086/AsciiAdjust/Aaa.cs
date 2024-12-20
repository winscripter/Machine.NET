using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void aaa(in Instruction instruction)
    {
        if (((ProcessorRegisters.Al & 0xFu) > 9u) || ProcessorRegisters.RFlagsAF)
        {
            ProcessorRegisters.Ax += 0x106;
            ProcessorRegisters.RFlagsAF = true;
            ProcessorRegisters.RFlagsCF = true;
        }
        else
        {
            ProcessorRegisters.RFlagsAF = false;
            ProcessorRegisters.RFlagsCF = false;
        }

        ProcessorRegisters.Al &= 0xF;
    }
}
