using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vextractf128(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Vextractf128_xmmm128_ymm_imm8:
                {
                    bool insertBelow = (byte)instruction.GetImmediate(2) == 0;
                    Vector256<float> ymm = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1));
                    OpKind op0Kind = instruction.GetOpKind(0);

                    if (insertBelow)
                    {
                        if (op0Kind == OpKind.Memory)
                        {
                            this.Memory.WriteBinaryVector128(GetMemOperand(in instruction), ymm.GetLower());
                        }
                        else
                        {
                            this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), ymm.GetLower());
                        }
                    }
                    else
                    {
                        if (op0Kind == OpKind.Memory)
                        {
                            this.Memory.WriteBinaryVector128(GetMemOperand(in instruction), ymm.GetUpper());
                        }
                        else
                        {
                            this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), ymm.GetUpper());
                        }
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
