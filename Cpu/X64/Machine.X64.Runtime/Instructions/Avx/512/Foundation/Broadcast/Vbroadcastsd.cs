using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vbroadcastsd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcastsd_ymm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        float value = Memory.ReadSingle(GetMemOperand32(instruction));
                        ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), Vector256.Create<float>(value));
                    }
                    else
                    {
                        float value = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1))[0];
                        ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), Vector256.Create<float>(value));
                    }

                    break;
                }

            case Code.EVEX_Vbroadcastsd_zmm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        float value = Memory.ReadSingle(GetMemOperand32(instruction));
                        ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), Vector512.Create<float>(value));
                    }
                    else
                    {
                        float value = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1))[0];
                        ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), Vector512.Create<float>(value));
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
