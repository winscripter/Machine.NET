using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmaddubsw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmaddubsw_mm_mmm64:
                {
                    Vector64<sbyte> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, sbyte>();
                    Vector64<sbyte> dst = GetVectorFromMM(in instruction, 1).As<ulong, sbyte>();

                    Vector64<short> result = Vector64<short>.Zero;
                    for (int i = 0; i < Vector64<short>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            Saturate.UInt16Signed(
                                src[2 * i] *
                                dst[2 * i] +
                                src[2 * i + 1] *
                                dst[2 * i + 1]
                            )
                        );
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Pmaddubsw_xmm_xmmm128:
                {
                    Vector128<short> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, short>();
                    Vector128<short> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, short>();

                    Vector128<short> result = Vector128<short>.Zero;
                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            Saturate.UInt16Signed(
                                src[2 * i] *
                                dst[2 * i] +
                                src[2 * i + 1] *
                                dst[2 * i + 1]
                            )
                        );
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
