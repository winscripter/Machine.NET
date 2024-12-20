using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void aam(in Instruction instruction)
    {
        byte value = (byte)instruction.GetImmediate(0);
        ProcessorRegisters.Ah = (byte)(ProcessorRegisters.Al / value);
        ProcessorRegisters.Al = (byte)(ProcessorRegisters.Al % value);
    }
}
