using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtdq2pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtdq2pd_xmm_xmmm64:
                {
                    (uint upper, uint lower) = (0u, 0u);
                    OpKind opKind = instruction.GetOpKind(1);
                    if (opKind == OpKind.Memory)
                    {
                        ulong u64AtOffset = this.Memory.ReadUInt64(GetMemOperand(in instruction));
                        (upper, lower) = (BitUtilities.GetLower32Bits(u64AtOffset), BitUtilities.GetUpper32Bits(u64AtOffset));
                    }
                    else if (opKind == OpKind.Register)
                    {
                        Vector128<uint> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, uint>();
                        (upper, lower) = (xmm[0], xmm[1]);
                    }
                    Vector128<double> destinationXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    destinationXmm = destinationXmm.WithElement(0, upper)
                                                   .WithElement(1, lower);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), destinationXmm.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
