using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovntdqa(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovntdqa_xmm_m128:
                {
                    this.ProcessorRegisters.SetXmm(
                        instruction.GetOpRegister(0),
                        this.Memory.ReadBinaryVector128(GetMemOperand(in instruction))
                    );
                    break;
                }

            case Code.EVEX_Vmovntdqa_ymm_m256:
                {
                    this.ProcessorRegisters.SetYmm(
                        instruction.GetOpRegister(0),
                        this.Memory.ReadBinaryVector256(GetMemOperand(in instruction))
                    );
                    break;
                }

            case Code.EVEX_Vmovntdqa_zmm_m512:
                {
                    this.ProcessorRegisters.SetZmm(
                        instruction.GetOpRegister(0),
                        this.Memory.ReadBinaryVector512(GetMemOperand(in instruction))
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
