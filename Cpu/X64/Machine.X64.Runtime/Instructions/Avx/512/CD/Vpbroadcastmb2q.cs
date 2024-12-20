using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpbroadcastmb2q(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpbroadcastmb2q_xmm_kr:
                {
                    ulong kr = this.ProcessorRegisters.EvaluateK(instruction.GetOpRegister(1));
                    Vector128<ulong> result =
                        Vector128.Create([
                            BitUtilities.IsBitSet(kr, 0) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 1) ? ulong.MaxValue : ulong.MinValue]);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastmb2q_ymm_kr:
                {
                    ulong kr = this.ProcessorRegisters.EvaluateK(instruction.GetOpRegister(1));
                    Vector256<ulong> result =
                        Vector256.Create([
                            BitUtilities.IsBitSet(kr, 0) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 1) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 2) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 3) ? ulong.MaxValue : ulong.MinValue]);

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastmb2q_zmm_kr:
                {
                    ulong kr = this.ProcessorRegisters.EvaluateK(instruction.GetOpRegister(1));
                    Vector512<ulong> result =
                        Vector512.Create([
                            BitUtilities.IsBitSet(kr, 0) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 1) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 2) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 3) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 4) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 5) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 6) ? ulong.MaxValue : ulong.MinValue,
                            BitUtilities.IsBitSet(kr, 7) ? ulong.MaxValue : ulong.MinValue]);

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
