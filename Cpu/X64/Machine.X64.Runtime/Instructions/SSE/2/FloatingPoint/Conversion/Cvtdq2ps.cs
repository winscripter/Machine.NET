using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtdq2ps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtdq2ps_xmm_xmmm128:
                {
                    Vector128<uint> vec = EvaluateXmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector128<float> destinationXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    destinationXmm = destinationXmm.WithElement(0, vec[0])
                                                   .WithElement(1, vec[1])
                                                   .WithElement(2, vec[2])
                                                   .WithElement(3, vec[3]);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), destinationXmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
