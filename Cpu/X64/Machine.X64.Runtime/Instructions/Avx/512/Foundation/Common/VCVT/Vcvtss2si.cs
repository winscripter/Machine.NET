using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtss2si(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtss2si_r32_xmmm32_er:
                {
                    float scalar = ReadXmmScalarOrSingle(in instruction, 1);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)scalar);
                    break;
                }

            case Code.EVEX_Vcvtss2si_r64_xmmm32_er:
                {
                    float scalar = ReadXmmScalarOrSingle(in instruction, 1);
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (uint)scalar);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
