using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtdq2pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtdq2pd_xmm_k1z_xmmm64b32:
                {
                    (uint upper, uint lower) = (0u, 0u);
                    OpKind opKind = instruction.GetOpKind(1);
                    if (opKind == OpKind.Memory)
                    {
                        ulong u64AtOffset = this.Memory.ReadUInt64(GetMemOperand64(in instruction));
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

            case Code.EVEX_Vcvtdq2pd_ymm_k1z_xmmm128b32:
                {
                    Vector128<uint> vec = EvaluateXmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector256<double> destinationYmm = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, double>();
                    destinationYmm = destinationYmm.WithElement(0, vec[0])
                                                   .WithElement(1, vec[1])
                                                   .WithElement(2, vec[2])
                                                   .WithElement(3, vec[3]);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), destinationYmm.As<double, float>());
                    break;
                }

            case Code.EVEX_Vcvtdq2pd_zmm_k1z_ymmm256b32_er:
                {
                    Vector256<uint> vec = EvaluateYmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector512<double> destinationZmm = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, double>();
                    destinationZmm = destinationZmm.WithElement(0, vec[0])
                                                   .WithElement(1, vec[1])
                                                   .WithElement(2, vec[2])
                                                   .WithElement(3, vec[3])
                                                   .WithElement(4, vec[4])
                                                   .WithElement(5, vec[5])
                                                   .WithElement(6, vec[6])
                                                   .WithElement(7, vec[7]);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), destinationZmm.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
