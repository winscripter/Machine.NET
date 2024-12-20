using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpopcntb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpopcntb_xmm_k1z_xmmm128:
                {
                    Vector128<byte> vec = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> result = Vector128.Create(
                        (byte)BitUtilities.PopCount(vec[0]),
                        (byte)BitUtilities.PopCount(vec[1]),
                        (byte)BitUtilities.PopCount(vec[2]),
                        (byte)BitUtilities.PopCount(vec[3]),
                        (byte)BitUtilities.PopCount(vec[4]),
                        (byte)BitUtilities.PopCount(vec[5]),
                        (byte)BitUtilities.PopCount(vec[6]),
                        (byte)BitUtilities.PopCount(vec[7]),
                        (byte)BitUtilities.PopCount(vec[8]),
                        (byte)BitUtilities.PopCount(vec[9]),
                        (byte)BitUtilities.PopCount(vec[10]),
                        (byte)BitUtilities.PopCount(vec[11]),
                        (byte)BitUtilities.PopCount(vec[12]),
                        (byte)BitUtilities.PopCount(vec[13]),
                        (byte)BitUtilities.PopCount(vec[14]),
                        (byte)BitUtilities.PopCount(vec[15])
                    );
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpopcntb_ymm_k1z_ymmm256:
                {
                    Vector256<byte> vec = EvaluateYmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector256<byte> result = Vector256.Create(
                        (byte)BitUtilities.PopCount(vec[0]),
                        (byte)BitUtilities.PopCount(vec[1]),
                        (byte)BitUtilities.PopCount(vec[2]),
                        (byte)BitUtilities.PopCount(vec[3]),
                        (byte)BitUtilities.PopCount(vec[4]),
                        (byte)BitUtilities.PopCount(vec[5]),
                        (byte)BitUtilities.PopCount(vec[6]),
                        (byte)BitUtilities.PopCount(vec[7]),
                        (byte)BitUtilities.PopCount(vec[8]),
                        (byte)BitUtilities.PopCount(vec[9]),
                        (byte)BitUtilities.PopCount(vec[10]),
                        (byte)BitUtilities.PopCount(vec[11]),
                        (byte)BitUtilities.PopCount(vec[12]),
                        (byte)BitUtilities.PopCount(vec[13]),
                        (byte)BitUtilities.PopCount(vec[14]),
                        (byte)BitUtilities.PopCount(vec[15]),
                        (byte)BitUtilities.PopCount(vec[16]),
                        (byte)BitUtilities.PopCount(vec[17]),
                        (byte)BitUtilities.PopCount(vec[18]),
                        (byte)BitUtilities.PopCount(vec[19]),
                        (byte)BitUtilities.PopCount(vec[20]),
                        (byte)BitUtilities.PopCount(vec[21]),
                        (byte)BitUtilities.PopCount(vec[22]),
                        (byte)BitUtilities.PopCount(vec[23]),
                        (byte)BitUtilities.PopCount(vec[24]),
                        (byte)BitUtilities.PopCount(vec[25]),
                        (byte)BitUtilities.PopCount(vec[26]),
                        (byte)BitUtilities.PopCount(vec[27]),
                        (byte)BitUtilities.PopCount(vec[28]),
                        (byte)BitUtilities.PopCount(vec[29]),
                        (byte)BitUtilities.PopCount(vec[30]),
                        (byte)BitUtilities.PopCount(vec[31])
                    );
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpopcntb_zmm_k1z_zmmm512:
                {
                    Vector512<byte> vec = EvaluateZmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector512<byte> result = Vector512.Create(
                        (byte)BitUtilities.PopCount(vec[0]),
                        (byte)BitUtilities.PopCount(vec[1]),
                        (byte)BitUtilities.PopCount(vec[2]),
                        (byte)BitUtilities.PopCount(vec[3]),
                        (byte)BitUtilities.PopCount(vec[4]),
                        (byte)BitUtilities.PopCount(vec[5]),
                        (byte)BitUtilities.PopCount(vec[6]),
                        (byte)BitUtilities.PopCount(vec[7]),
                        (byte)BitUtilities.PopCount(vec[8]),
                        (byte)BitUtilities.PopCount(vec[9]),
                        (byte)BitUtilities.PopCount(vec[10]),
                        (byte)BitUtilities.PopCount(vec[11]),
                        (byte)BitUtilities.PopCount(vec[12]),
                        (byte)BitUtilities.PopCount(vec[13]),
                        (byte)BitUtilities.PopCount(vec[14]),
                        (byte)BitUtilities.PopCount(vec[15]),
                        (byte)BitUtilities.PopCount(vec[16]),
                        (byte)BitUtilities.PopCount(vec[17]),
                        (byte)BitUtilities.PopCount(vec[18]),
                        (byte)BitUtilities.PopCount(vec[19]),
                        (byte)BitUtilities.PopCount(vec[20]),
                        (byte)BitUtilities.PopCount(vec[21]),
                        (byte)BitUtilities.PopCount(vec[22]),
                        (byte)BitUtilities.PopCount(vec[23]),
                        (byte)BitUtilities.PopCount(vec[24]),
                        (byte)BitUtilities.PopCount(vec[25]),
                        (byte)BitUtilities.PopCount(vec[26]),
                        (byte)BitUtilities.PopCount(vec[27]),
                        (byte)BitUtilities.PopCount(vec[28]),
                        (byte)BitUtilities.PopCount(vec[29]),
                        (byte)BitUtilities.PopCount(vec[30]),
                        (byte)BitUtilities.PopCount(vec[31]),
                        (byte)BitUtilities.PopCount(vec[32]),
                        (byte)BitUtilities.PopCount(vec[33]),
                        (byte)BitUtilities.PopCount(vec[34]),
                        (byte)BitUtilities.PopCount(vec[35]),
                        (byte)BitUtilities.PopCount(vec[36]),
                        (byte)BitUtilities.PopCount(vec[37]),
                        (byte)BitUtilities.PopCount(vec[38]),
                        (byte)BitUtilities.PopCount(vec[39]),
                        (byte)BitUtilities.PopCount(vec[40]),
                        (byte)BitUtilities.PopCount(vec[41]),
                        (byte)BitUtilities.PopCount(vec[42]),
                        (byte)BitUtilities.PopCount(vec[43]),
                        (byte)BitUtilities.PopCount(vec[44]),
                        (byte)BitUtilities.PopCount(vec[45]),
                        (byte)BitUtilities.PopCount(vec[46]),
                        (byte)BitUtilities.PopCount(vec[47]),
                        (byte)BitUtilities.PopCount(vec[48]),
                        (byte)BitUtilities.PopCount(vec[49]),
                        (byte)BitUtilities.PopCount(vec[50]),
                        (byte)BitUtilities.PopCount(vec[51]),
                        (byte)BitUtilities.PopCount(vec[52]),
                        (byte)BitUtilities.PopCount(vec[53]),
                        (byte)BitUtilities.PopCount(vec[54]),
                        (byte)BitUtilities.PopCount(vec[55]),
                        (byte)BitUtilities.PopCount(vec[56]),
                        (byte)BitUtilities.PopCount(vec[57]),
                        (byte)BitUtilities.PopCount(vec[58]),
                        (byte)BitUtilities.PopCount(vec[59]),
                        (byte)BitUtilities.PopCount(vec[60]),
                        (byte)BitUtilities.PopCount(vec[61]),
                        (byte)BitUtilities.PopCount(vec[62]),
                        (byte)BitUtilities.PopCount(vec[63])
                    );
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
