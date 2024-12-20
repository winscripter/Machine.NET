using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void aad(in Instruction instruction)
    {
        byte value = (byte)instruction.GetImmediate(0);
        ProcessorRegisters.Al += (byte)(ProcessorRegisters.Al * value);
        ProcessorRegisters.Ah = 0;
        ProcessorRegisters.UpdateLowerEightBitsOfFlagsRegister(ProcessorRegisters.Al);
    }
}
