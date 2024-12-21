using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vbroadcasti32x2(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcasti32x2_xmm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        uint value1 = Memory.ReadUInt32(GetMemOperand64(in instruction));
                        uint value2 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 4);
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            Vector128.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<uint, float>()
                        );
                    }
                    else
                    {
                        Vector128<uint> vec = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, uint>();
                        uint value1 = vec[0];
                        uint value2 = vec[1];
                        ProcessorRegisters.SetXmm(
                            instruction.GetOpRegister(0),
                            Vector128.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<uint, float>()
                        );
                    }
                    break;
                }

            case Code.EVEX_Vbroadcasti32x2_ymm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        uint value1 = Memory.ReadUInt32(GetMemOperand64(in instruction));
                        uint value2 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 4);
                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Vector256.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<uint, float>()
                        );
                    }
                    else
                    {
                        Vector256<uint> vec = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, uint>();
                        uint value1 = vec[0];
                        uint value2 = vec[1];
                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Vector256.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                ]
                            )
                            .As<uint, float>()
                        );
                    }
                    break;
                }

            case Code.EVEX_Vbroadcasti32x2_zmm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        uint value1 = Memory.ReadUInt32(GetMemOperand64(in instruction));
                        uint value2 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 4);
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Vector512.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<uint, float>()
                        );
                    }
                    else
                    {
                        Vector512<uint> vec = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, uint>();
                        uint value1 = vec[0];
                        uint value2 = vec[1];
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Vector512.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<uint, float>()
                        );
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
