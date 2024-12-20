using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pavgw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pavgw_mm_mmm64:
                {
                    Vector64<ushort> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, ushort>();
                    Vector64<ushort> dst = GetVectorFromMM(in instruction, 0).As<ulong, ushort>();
                    Vector64<ushort> result = Vector64<ushort>.Zero;

                    for (int i = 0; i < Vector64<ushort>.Count; i++)
                    {
                        result = result.WithElement(i, (ushort)((dst[i] + src[i] + 1) >> 1));
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Pavgw_xmm_xmmm128:
                {
                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();
                    Vector128<ushort> result = Vector128<ushort>.Zero;

                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                    {
                        result = result.WithElement(i, (ushort)((dst[i] + src[i] + 1) >> 1));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
