using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvtpd2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvtpd2dq_xmm_k1z_xmmm128b64:
                {
                    Vector128<double> vec = EvaluateXmmFromInstruction(in instruction, 1).AsDouble();
                    Vector128<ulong> destinationXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    destinationXmm = destinationXmm.WithElement(0, (ulong)vec[0])
                                                   .WithElement(1, (ulong)vec[1]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), destinationXmm.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vcvtpd2dq_xmm_k1z_ymmm256b64:
                {
                    Vector256<double> vec = EvaluateYmmFromInstruction(in instruction, 1).AsDouble();
                    Vector256<ulong> destinationYmm = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    destinationYmm = destinationYmm.WithElement(0, (ulong)vec[0])
                                                   .WithElement(1, (ulong)vec[1])
                                                   .WithElement(2, (ulong)vec[2])
                                                   .WithElement(3, (ulong)vec[3]);
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), destinationYmm.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vcvtpd2dq_ymm_k1z_zmmm512b64_er:
                {
                    Vector512<double> vec = EvaluateZmmFromInstruction(in instruction, 1).AsDouble();
                    Vector512<ulong> destinationZmm = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    destinationZmm = destinationZmm.WithElement(0, (ulong)vec[0])
                                                   .WithElement(1, (ulong)vec[1])
                                                   .WithElement(2, (ulong)vec[2])
                                                   .WithElement(3, (ulong)vec[3])
                                                   .WithElement(4, (ulong)vec[4])
                                                   .WithElement(5, (ulong)vec[5])
                                                   .WithElement(6, (ulong)vec[6])
                                                   .WithElement(7, (ulong)vec[7]);
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), destinationZmm.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
