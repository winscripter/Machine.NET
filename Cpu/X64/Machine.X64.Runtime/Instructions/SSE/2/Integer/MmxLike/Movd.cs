using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movd_mm_rm32:
                {
                    uint value = RMEvaluate32(in instruction, 1);
                    ulong mm = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0));
                    BitUtilities.SetLower32Bits(ref mm, value);
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mm);
                    break;
                }

            case Code.Movd_rm32_mm:
                {
                    ulong mm = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1));
                    this.RMSet32(in instruction, BitUtilities.GetLower32Bits(mm), 0);
                    break;
                }

            case Code.Movd_rm32_xmm:
                {
                    Vector128<uint> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, uint>();
                    RMSet32(in instruction, vec.ToScalar(), 0);
                    break;
                }

            case Code.Movd_xmm_rm32:
                {
                    uint rm = RMEvaluate32(in instruction, 1);
                    Vector128<uint> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();
                    vec = vec.WithElement(0, rm);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
