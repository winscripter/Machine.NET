using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pcmpgtw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pcmpgtw_mm_mmm64:
                {
                    Vector64<ushort> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, ushort>();
                    Vector64<ushort> dst = GetVectorFromMM(in instruction, 1).As<ulong, ushort>();

                    Vector64<ushort> result = Vector64<ushort>.Zero;
                    for (int i = 0; i < Vector64<ushort>.Count; i++)
                    {
                        result = result.WithElement(i, src[i] > dst[i] ? ushort.MaxValue : (ushort)0x00);
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Pcmpgtw_xmm_xmmm128:
                {
                    Vector128<ushort> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();

                    Vector128<ushort> result = Vector128<ushort>.Zero;
                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                    {
                        result = result.WithElement(i, src[i] > dst[i] ? ushort.MaxValue : (ushort)0x00);
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
