using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpbroadcastmw2d(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpbroadcastmw2d_xmm_kr:
                {
                    ulong kr = this.ProcessorRegisters.EvaluateK(instruction.GetOpRegister(1));
                    Vector128<uint> result =
                        Vector128.Create([
                            BitUtilities.IsBitSet(kr, 0) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 1) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 2) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 3) ? uint.MaxValue : uint.MinValue]);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastmb2q_ymm_kr:
                {
                    ulong kr = this.ProcessorRegisters.EvaluateK(instruction.GetOpRegister(1));
                    Vector256<uint> result =
                        Vector256.Create([
                            BitUtilities.IsBitSet(kr, 0) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 1) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 2) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 3) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 4) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 5) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 6) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 7) ? uint.MaxValue : uint.MinValue]);

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastmb2q_zmm_kr:
                {
                    ulong kr = this.ProcessorRegisters.EvaluateK(instruction.GetOpRegister(1));
                    Vector512<uint> result =
                        Vector512.Create([
                            BitUtilities.IsBitSet(kr, 0) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 1) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 2) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 3) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 4) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 5) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 6) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 7) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 8) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 9) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 10) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 11) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 12) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 13) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 14) ? uint.MaxValue : uint.MinValue,
                            BitUtilities.IsBitSet(kr, 15) ? uint.MaxValue : uint.MinValue]);

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
