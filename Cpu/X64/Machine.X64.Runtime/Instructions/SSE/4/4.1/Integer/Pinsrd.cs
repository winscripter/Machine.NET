using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pinsrd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pinsrd_xmm_rm32_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    uint valueToInsert = RMEvaluate32(in instruction, 1);

                    Vector128<uint> mmVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), mmVector.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
