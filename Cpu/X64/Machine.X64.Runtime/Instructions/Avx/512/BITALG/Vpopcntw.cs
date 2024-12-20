using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpopcntw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpopcntw_xmm_k1z_xmmm128:
                {
                    Vector128<ushort> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector128<ushort> result = Vector128.Create(
                        (ushort)BitUtilities.PopCount(vec[0]),
                        (ushort)BitUtilities.PopCount(vec[1]),
                        (ushort)BitUtilities.PopCount(vec[2]),
                        (ushort)BitUtilities.PopCount(vec[3]),
                        (ushort)BitUtilities.PopCount(vec[4]),
                        (ushort)BitUtilities.PopCount(vec[5]),
                        (ushort)BitUtilities.PopCount(vec[6]),
                        (ushort)BitUtilities.PopCount(vec[7])
                    );
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpopcntw_ymm_k1z_ymmm256:
                {
                    Vector256<ushort> vec = EvaluateYmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector256<ushort> result = Vector256.Create(
                        (ushort)BitUtilities.PopCount(vec[0]),
                        (ushort)BitUtilities.PopCount(vec[1]),
                        (ushort)BitUtilities.PopCount(vec[2]),
                        (ushort)BitUtilities.PopCount(vec[3]),
                        (ushort)BitUtilities.PopCount(vec[4]),
                        (ushort)BitUtilities.PopCount(vec[5]),
                        (ushort)BitUtilities.PopCount(vec[6]),
                        (ushort)BitUtilities.PopCount(vec[7]),
                        (ushort)BitUtilities.PopCount(vec[8]),
                        (ushort)BitUtilities.PopCount(vec[9]),
                        (ushort)BitUtilities.PopCount(vec[10]),
                        (ushort)BitUtilities.PopCount(vec[11]),
                        (ushort)BitUtilities.PopCount(vec[12]),
                        (ushort)BitUtilities.PopCount(vec[13]),
                        (ushort)BitUtilities.PopCount(vec[14]),
                        (ushort)BitUtilities.PopCount(vec[15])
                    );
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpopcntw_zmm_k1z_zmmm512:
                {
                    Vector512<ushort> vec = EvaluateZmmFromInstruction(in instruction, 1).As<float, ushort>();
                    Vector512<ushort> result = Vector512.Create(
                        (ushort)BitUtilities.PopCount(vec[0]),
                        (ushort)BitUtilities.PopCount(vec[1]),
                        (ushort)BitUtilities.PopCount(vec[2]),
                        (ushort)BitUtilities.PopCount(vec[3]),
                        (ushort)BitUtilities.PopCount(vec[4]),
                        (ushort)BitUtilities.PopCount(vec[5]),
                        (ushort)BitUtilities.PopCount(vec[6]),
                        (ushort)BitUtilities.PopCount(vec[7]),
                        (ushort)BitUtilities.PopCount(vec[8]),
                        (ushort)BitUtilities.PopCount(vec[9]),
                        (ushort)BitUtilities.PopCount(vec[10]),
                        (ushort)BitUtilities.PopCount(vec[11]),
                        (ushort)BitUtilities.PopCount(vec[12]),
                        (ushort)BitUtilities.PopCount(vec[13]),
                        (ushort)BitUtilities.PopCount(vec[14]),
                        (ushort)BitUtilities.PopCount(vec[15]),
                        (ushort)BitUtilities.PopCount(vec[16]),
                        (ushort)BitUtilities.PopCount(vec[17]),
                        (ushort)BitUtilities.PopCount(vec[18]),
                        (ushort)BitUtilities.PopCount(vec[19]),
                        (ushort)BitUtilities.PopCount(vec[20]),
                        (ushort)BitUtilities.PopCount(vec[21]),
                        (ushort)BitUtilities.PopCount(vec[22]),
                        (ushort)BitUtilities.PopCount(vec[23]),
                        (ushort)BitUtilities.PopCount(vec[24]),
                        (ushort)BitUtilities.PopCount(vec[25]),
                        (ushort)BitUtilities.PopCount(vec[26]),
                        (ushort)BitUtilities.PopCount(vec[27]),
                        (ushort)BitUtilities.PopCount(vec[28]),
                        (ushort)BitUtilities.PopCount(vec[29]),
                        (ushort)BitUtilities.PopCount(vec[30]),
                        (ushort)BitUtilities.PopCount(vec[31])
                    );
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
