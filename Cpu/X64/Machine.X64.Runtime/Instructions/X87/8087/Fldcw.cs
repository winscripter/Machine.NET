using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void fldcw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Fldcw_m2byte:
                {
                    ushort controlWord = this.Memory.ReadUInt16(GetMemOperand64(instruction));
                    this.Fpu.ControlWord = controlWord;
                    break;
                }
                
            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
