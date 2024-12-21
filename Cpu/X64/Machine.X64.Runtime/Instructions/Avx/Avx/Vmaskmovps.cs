using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmaskmovps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Vmaskmovps_m128_xmm_xmm:
                {
                    Vector128<float> xmm1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> xmm2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2));

                    ulong baseAddress = GetMemOperand64(in instruction);

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        float mask = xmm2[i];
                        
                        if (((ulong)mask & 0x80) == 0)
                        {
                            // In conditions where mask & 0x80 does not result in 0,
                            // the item in the address is either kept as zero or what
                            // it used to be prior to the invocation of the instruction.
                            this.Memory.WriteSingle(baseAddress + (ulong)(i * 4), xmm1[i]);
                        }
                    }

                    break;
                }

            case Code.VEX_Vmaskmovps_m256_ymm_ymm:
                {
                    Vector256<float> xmm1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    Vector256<float> xmm2 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(2));

                    ulong baseAddress = GetMemOperand64(in instruction);

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        float mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            // In conditions where mask & 0x80 does not result in 0,
                            // the item in the address is either kept as zero or what
                            // it used to be prior to the invocation of the instruction.
                            this.Memory.WriteSingle(baseAddress + (ulong)(i * 4), xmm1[i]);
                        }
                    }

                    break;
                }

            case Code.VEX_Vmaskmovps_xmm_xmm_m128:
                {
                    Vector128<float> xmm1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector128<float> xmm2 = this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction));
                    Vector128<float> result = Vector128<float>.Zero;

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        float mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            result = result.WithElement(i, xmm1[i]);
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);

                    break;
                }

            case Code.VEX_Vmaskmovps_ymm_ymm_m256:
                {
                    Vector256<float> xmm1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    Vector256<float> xmm2 = this.Memory.ReadBinaryVector256(GetMemOperand64(in instruction));
                    Vector256<float> result = Vector256<float>.Zero;

                    for (int i = 0; i < Vector256<float>.Count; i++)
                    {
                        float mask = xmm2[i];

                        if (((ulong)mask & 0x80) == 0)
                        {
                            result = result.WithElement(i, xmm1[i]);
                        }
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
