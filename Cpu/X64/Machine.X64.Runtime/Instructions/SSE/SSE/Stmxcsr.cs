using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void stmxcsr(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Stmxcsr_m32:
                {
                    uint mxcsr = this.Memory.ReadUInt32(GetMemOperand64(instruction));
                    // Seriously, C#??? I can't do "this.Mxcsr.Value = mxcsr"???
                    var actualMXCSR = this.Mxcsr;
                    actualMXCSR.Value = mxcsr;
                    this.Mxcsr = actualMXCSR;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
