using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void tzcnt(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Tzcnt_r32_rm32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 1);
                    uint result = (uint)(31 + ~rm32
                                      - (uint)(((rm32 & -rm32 & 0x0000FFFF) == 0) ? 16 : 0)
                                      - (uint)(((rm32 & -rm32 & 0x00FF00FF) == 0) ? 8 : 0)
                                      - (uint)(((rm32 & -rm32 & 0x0F0F0F0F) == 0) ? 4 : 0)
                                      - (uint)(((rm32 & -rm32 & 0x33333333) == 0) ? 2 : 0)
                                      - (uint)(((rm32 & -rm32 & 0x55555555) == 0) ? 1 : 0)
                                   );
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
