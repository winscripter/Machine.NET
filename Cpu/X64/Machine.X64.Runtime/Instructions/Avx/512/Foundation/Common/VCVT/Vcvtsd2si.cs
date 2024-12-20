using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtsd2si(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtsd2si_r32_xmmm64_er:
                {
                    double scalar = ReadXmmScalarOrDouble(in instruction, 1);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)(int)scalar);
                    break;
                }

            case Code.EVEX_Vcvtsd2si_r64_xmmm64_er:
                {
                    double scalar = ReadXmmScalarOrDouble(in instruction, 1);
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)(long)scalar);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
