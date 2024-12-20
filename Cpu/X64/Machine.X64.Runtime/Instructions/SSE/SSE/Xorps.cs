using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void xorps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Xorps_xmm_xmmm128:
                {
                    Register destination = instruction.GetOpRegister(0);
                    Vector128<float> dest = ProcessorRegisters.EvaluateXmm(destination);
                    Vector128<float> src = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> result = dest ^ src; // XOR on vector XORs everything inside
                    ProcessorRegisters.SetXmm(destination, result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
