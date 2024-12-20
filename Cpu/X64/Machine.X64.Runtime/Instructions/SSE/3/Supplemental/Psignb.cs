using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psignb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psignb_mm_mmm64:
                {
                    Vector64<sbyte> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, sbyte>();
                    Vector64<sbyte> dst = GetVectorFromMM(in instruction, 0).As<ulong, sbyte>();

                    Vector64<sbyte> result = Vector64<sbyte>.Zero;
                    for (int i = 0; i < Vector64<sbyte>.Count; i++)
                    {
                        result = result.WithElement(i, dst[i] < 0 ? (sbyte)-src[i] : src[i]);
                    }

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<sbyte, ulong>()[0]);
                    break;
                }

            case Code.Psignb_xmm_xmmm128:
                {
                    Vector128<sbyte> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, sbyte>();
                    Vector128<sbyte> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, sbyte>();

                    Vector128<sbyte> result = Vector128<sbyte>.Zero;
                    for (int i = 0; i < Vector128<sbyte>.Count; i++)
                    {
                        result = result.WithElement(i, dst[i] < 0 ? (sbyte)-src[i] : src[i]);
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<sbyte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
