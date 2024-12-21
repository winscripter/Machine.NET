using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movntdqa(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movntdqa_xmm_m128:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        this.Memory.ReadBinaryVector128(GetMemOperand(in instruction))
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
