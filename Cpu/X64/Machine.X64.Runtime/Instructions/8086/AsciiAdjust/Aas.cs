using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void aas(in Instruction instruction)
    {
        if (((ProcessorRegisters.Al & 0xFu) > 9u) || ProcessorRegisters.RFlagsAF)
        {
            ProcessorRegisters.Ax -= 6;
            ProcessorRegisters.Al--;
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
