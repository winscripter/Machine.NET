using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vinsertf128(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Vinsertf128_ymm_ymm_xmmm128_imm8:
                {
                    bool insertBelow = (byte)instruction.GetImmediate(3) == 0;
                    Vector256<float> ymm = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    Vector128<float> xmm = EvaluateXmmFromInstruction(in instruction, 2);

                    switch (insertBelow)
                    {
                        case true:
                            ymm = ymm.WithLower(xmm);
                            break;

                        case false:
                            ymm = ymm.WithUpper(xmm);
                            break;
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), ymm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
