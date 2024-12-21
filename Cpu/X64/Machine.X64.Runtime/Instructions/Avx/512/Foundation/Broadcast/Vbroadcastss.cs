using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vbroadcastss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcastss_xmm_k1z_xmmm32:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        float value = Memory.ReadSingle(GetMemOperand32(in instruction));
                        ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), Vector128.Create<float>(value));
                    }
                    else
                    {
                        float value = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1))[0];
                        ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), Vector128.Create<float>(value));
                    }

                    break;
                }

            case Code.EVEX_Vbroadcastss_ymm_k1z_xmmm32:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        float value = Memory.ReadSingle(GetMemOperand32(in instruction));
                        ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), Vector256.Create<float>(value));
                    }
                    else
                    {
                        float value = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1))[0];
                        ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), Vector256.Create<float>(value));
                    }

                    break;
                }

            case Code.EVEX_Vbroadcastss_zmm_k1z_xmmm32:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        float value = Memory.ReadSingle(GetMemOperand32(in instruction));
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
