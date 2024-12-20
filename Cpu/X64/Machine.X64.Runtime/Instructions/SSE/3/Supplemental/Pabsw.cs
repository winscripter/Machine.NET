using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pabsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pabsw_mm_mmm64:
                {
                    Vector64<short> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, short>();

                    Vector64<short> result = Vector64<short>.Zero;
                    for (int i = 0; i < Vector64<short>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Abs(src[i]));
                    }

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<short, ulong>()[0]);
                    break;
                }

            case Code.Pabsw_xmm_xmmm128:
                {
                    Vector128<short> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, short>();

                    Vector128<short> result = Vector128<short>.Zero;
                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        result = result.WithElement(i, Math.Abs(src[i]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<short, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
