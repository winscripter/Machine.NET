using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movmskps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movmskps_r32_xmm:
                {
                    Vector128<float> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Register dest = instruction.GetOpRegister(0);
                    uint destData = this.ProcessorRegisters.EvaluateRegisterValue32(dest);

                    for (int i = 0; i < Vector128<float>.Count; i++)
                    {
                        BitUtilities.SetBit(ref destData, i, RealHelpers.GetSign(vec[i]));
                    }

                    this.ProcessorRegisters.WriteToRegister32(dest, destData);
                    break;
                }

            case Code.Movmskps_r64_xmm:
                {
                    Vector128<float> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    Register dest = instruction.GetOpRegister(0);
                    ulong destData = this.ProcessorRegisters.EvaluateRegisterValue64(dest);

                    for (int i = 0; i < Vector128<float>.Count; i++)
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
