using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pextrq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pextrq_rm64_xmm_imm8:
                {
                    byte imm8 = (byte)instruction.GetImmediate(2);
                    if (imm8 > 15)
                    {
                        RaiseException(StaticErrors.GeneralProtectionFault);
                        break;
                    }

                    Vector128<ulong> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    RMSet64(in instruction, xmm[imm8]);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
