using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtdq2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtdq2ps_xmm_k1z_xmmm128b32:
                {
                    Vector128<uint> vec = EvaluateXmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector128<float> destinationXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    destinationXmm = destinationXmm.WithElement(0, vec[0])
                                                   .WithElement(1, vec[1])
                                                   .WithElement(2, vec[2])
                                                   .WithElement(3, vec[3]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), destinationXmm);
                    break;
                }

            case Code.EVEX_Vcvtdq2ps_ymm_k1z_ymmm256b32:
                {
                    Vector256<uint> vec = EvaluateYmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector256<float> destinationYmm = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0));
                    destinationYmm = destinationYmm.WithElement(0, vec[0])
                                                   .WithElement(1, vec[1])
                                                   .WithElement(2, vec[2])
                                                   .WithElement(3, vec[3])
                                                   .WithElement(4, vec[4])
                                                   .WithElement(5, vec[5])
                                                   .WithElement(6, vec[6])
                                                   .WithElement(7, vec[7]);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), destinationYmm);
                    break;
                }

            case Code.EVEX_Vcvtdq2ps_zmm_k1z_zmmm512b32_er:
                {
                    Vector512<uint> vec = EvaluateZmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector512<float> destinationZmm = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0));
                    destinationZmm = destinationZmm.WithElement(0, vec[0])
                                                   .WithElement(1, vec[1])
                                                   .WithElement(2, vec[2])
                                                   .WithElement(3, vec[3])
                                                   .WithElement(4, vec[4])
                                                   .WithElement(5, vec[5])
                                                   .WithElement(6, vec[6])
                                                   .WithElement(7, vec[7])
                                                   .WithElement(8, vec[8])
                                                   .WithElement(9, vec[9])
                                                   .WithElement(10, vec[10])
                                                   .WithElement(11, vec[11])
                                                   .WithElement(12, vec[12])
                                                   .WithElement(13, vec[13])
                                                   .WithElement(14, vec[14])
                                                   .WithElement(15, vec[15]);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), destinationZmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
