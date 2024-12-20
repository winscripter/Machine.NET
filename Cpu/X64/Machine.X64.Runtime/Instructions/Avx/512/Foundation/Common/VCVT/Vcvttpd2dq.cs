using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vcvttpd2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vcvttpd2dq_xmm_k1z_xmmm128b64:
                {
                    Vector128<double> packedDouble = EvaluateXmmFromInstruction(in instruction, 1).AsDouble();
                    Vector128<ulong> result = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsUInt64();
                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0u);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (ulong)packedDouble[i]);
                        }
                    }
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vcvttpd2dq_xmm_k1z_ymmm256b64:
                {
                    Vector256<double> packedDouble = EvaluateYmmFromInstruction(in instruction, 1).AsDouble();
                    Vector256<ulong> result = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).AsUInt64();
                    for (int i = 0; i < Vector256<double>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0u);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (ulong)packedDouble[i]);
                        }
                    }
                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            case Code.EVEX_Vcvttpd2dq_ymm_k1z_zmmm512b64_sae:
                {
                    Vector512<double> packedDouble = EvaluateZmmFromInstruction(in instruction, 1).AsDouble();
                    Vector512<ulong> result = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).AsUInt64();
                    for (int i = 0; i < Vector512<double>.Count; i++)
                    {
                        if (HasBitSetInK1(i))
                        {
                            if (instruction.ZeroingMasking)
                            {
                                result = result.WithElement(i, 0u);
                            }
                        }
                        else
                        {
                            result = result.WithElement(i, (ulong)packedDouble[i]);
                        }
                    }
                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
