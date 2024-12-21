using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovsd_m64_k1_xmm:
                {
                    Vector128<double> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsDouble();
                    double scalar = xmm.GetElement(0);
                    this.Memory.WriteDouble(GetMemOperand(in instruction), scalar);
                    break;
                }

            case Code.EVEX_Vmovsd_xmm_k1z_m64:
                {
                    Vector128<double> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).AsDouble();
                    double scalar = this.Memory.ReadDouble(GetMemOperand(in instruction));
                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (this.HasBitSetInK1(i) && instruction.OpMask != Register.None)
                        {
                            if (instruction.ZeroingMasking)
                            {
                                xmm = xmm.WithElement(i, 0);
                                continue;
                            }
                            continue;
                        }
                        if (i == 0)
                        {
                            xmm = xmm.WithElement(i, scalar);
                        }
                    }
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm.AsSingle());
                    break;
                }

            case Code.EVEX_Vmovsd_xmm_k1z_xmm_xmm:
                {
                    Vector128<double> parameter3 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).AsDouble();
                    Vector128<double> parameter2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsDouble();

                    // First parameter (parameter1) is left unaffected, it's only use is to
                    // be set as the result.

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        if (this.HasBitSetInK1(i) && instruction.OpMask != Register.None)
                        {
                            if (instruction.ZeroingMasking)
                            {
                                parameter2 = parameter2.WithElement(i, 0);
                                continue;
                            }
                            continue;
                        }
                        else if (i == 0)
                        {
                            parameter2 = parameter2.WithElement(0, parameter3.ToScalar());
                            continue;
                        }
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), parameter2.AsSingle());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
