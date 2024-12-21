using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovss_m32_k1_xmm:
                {
                    float scalar = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).ToScalar();
                    this.Memory.WriteSingle(GetMemOperand32(in instruction), scalar);
                    break;
                }

            case Code.EVEX_Vmovss_xmm_k1z_m32:
                {
                    AlterScalarOfXmm(instruction.GetOpRegister(0), Memory.ReadSingle(GetMemOperand32(in instruction)));
                    break;
                }

            case Code.EVEX_Vmovss_xmm_k1z_xmm_xmm:
                {
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    float scalar = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).ToScalar();
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

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
