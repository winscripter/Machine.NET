using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovhpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovhpd_m64_xmm:
                {
                    Vector128<double> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector64<double> low = xmm.GetUpper();
                    this.Memory.WriteBinaryVector64(GetMemOperand(in instruction), low);
                    break;
                }

            case Code.EVEX_Vmovhpd_xmm_xmm_m64:
                {
                    Vector64<double> vec = this.Memory.ReadBinaryVector64(GetMemOperand(in instruction)).As<float, double>();
                    Vector128<double> fetchedXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    fetchedXmm = fetchedXmm.WithUpper(vec);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), fetchedXmm.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
