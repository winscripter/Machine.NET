using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vinsertps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vinsertps_xmm_xmm_xmmm32_imm8:
                {
                    Vector128<float> dest = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> src = EvaluateXmmFromInstruction(in instruction, 1);
                    byte control = (byte)instruction.GetImmediate(2);

                    int srcIndex = (control >> 6) & 0b11;
                    int destIndex = (control >> 4) & 0b11;
                    int zeroMask = control & 0b1111;

                    dest = dest.WithElement(destIndex, src[srcIndex]);

                    for (int i = 0; i < 4; i++)
                    {
                        if ((zeroMask & (1 << i)) != 0)
                        {
                            dest = dest.WithElement(i, 0F);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), dest);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
