using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movmskpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movmskpd_r32_xmm:
                {
                    Vector128<double> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Register dest = instruction.GetOpRegister(0);
                    uint destData = this.ProcessorRegisters.EvaluateRegisterValue32(dest);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        BitUtilities.SetBit(ref destData, i, RealHelpers.GetSign(vec[i]));
                    }

                    this.ProcessorRegisters.WriteToRegister32(dest, destData);
                    break;
                }

            case Code.Movmskpd_r64_xmm:
                {
                    Vector128<double> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Register dest = instruction.GetOpRegister(0);
                    ulong destData = this.ProcessorRegisters.EvaluateRegisterValue64(dest);

                    for (int i = 0; i < Vector128<double>.Count; i++)
                    {
                        BitUtilities.SetBit(ref destData, i, RealHelpers.GetSign(vec[i]));
                    }

                    this.ProcessorRegisters.WriteToRegister64(dest, destData);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
