using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovhps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovhps_m64_xmm:
                {
                    Vector128<float> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Vector64<float> low = xmm.GetUpper();
                    this.Memory.WriteBinaryVector64(GetMemOperand64(instruction), low);
                    break;
                }

            case Code.EVEX_Vmovhps_xmm_xmm_m64:
                {
                    Vector64<float> vec = this.Memory.ReadBinaryVector64(GetMemOperand64(instruction));
                    Vector128<float> fetchedXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    fetchedXmm = fetchedXmm.WithUpper(vec);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), fetchedXmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
