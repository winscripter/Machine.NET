using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void orps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Orps_xmm_xmmm128:
                {
                    Register destination = instruction.GetOpRegister(0);
                    Vector128<float> dest = ProcessorRegisters.EvaluateXmm(destination);
                    Vector128<float> src = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = dest | src; // OR on vector ORs everything inside
                    ProcessorRegisters.SetXmm(destination, result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
