using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pextrd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pextrd_rm32_xmm_imm8:
                {
                    byte imm8 = (byte)instruction.GetImmediate(2);
                    if (imm8 > 15)
                    {
                        RaiseException(StaticErrors.GeneralProtectionFault);
                        break;
                    }

                    Vector128<uint> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, uint>();
                    RMSet32(in instruction, xmm[imm8]);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
