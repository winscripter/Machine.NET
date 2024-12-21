using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmaskmovpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Vmaskmovpd_m128_xmm_xmm:
                {
                    Vector128<double> xmm1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> xmm2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, double>();

                    ulong baseAddress = GetMemOperand(in instruction);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        double mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            // In conditions where mask & 0x80 does not result in 0,
                            // the item in the address is either kept as zero or what
                            // it used to be prior to the invocation of the instruction.
                            this.Memory.WriteDouble(baseAddress + (ulong)(i * 8), xmm1[i]);
                        }
                    }

                    break;
                }

            case Code.VEX_Vmaskmovpd_m256_ymm_ymm:
                {
                    Vector256<double> xmm1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector256<double> xmm2 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2)).As<float, double>();

                    ulong baseAddress = GetMemOperand(in instruction);

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        double mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            // In conditions where mask & 0x80 does not result in 0,
                            // the item in the address is either kept as zero or what
                            // it used to be prior to the invocation of the instruction.
                            this.Memory.WriteDouble(baseAddress + (ulong)(i * 8), xmm1[i]);
                        }
                    }

                    break;
                }

            case Code.VEX_Vmaskmovpd_xmm_xmm_m128:
                {
                    Vector128<double> xmm1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector128<double> xmm2 = this.Memory.ReadBinaryVector128(GetMemOperand(in instruction)).As<float, double>();
                    Vector128<double> result = Vector128<double>.Zero;

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        double mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            result = result.WithElement(i, xmm1[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());

                    break;
                }

            case Code.VEX_Vmaskmovpd_ymm_ymm_m256:
                {
                    Vector256<double> xmm1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector256<double> xmm2 = this.Memory.ReadBinaryVector256(GetMemOperand(in instruction)).As<float, double>();
                    Vector256<double> result = Vector256<double>.Zero;

                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        double mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            result = result.WithElement(i, xmm1[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
