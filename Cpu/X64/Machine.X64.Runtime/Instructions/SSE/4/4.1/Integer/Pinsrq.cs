using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pinsrq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pinsrq_xmm_rm64_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    ulong valueToInsert = RMEvaluate64(in instruction, 1);

                    Vector128<ulong> mmVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), mmVector.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
