using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;
public partial class CpuRuntime
{
    private void vbroadcasti32x4(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcasti32x4_ymm_k1z_m128:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        uint value1 = Memory.ReadUInt32(GetMemOperand64(instruction));
                        uint value2 = Memory.ReadUInt32(GetMemOperand64(instruction) + 4);
                        uint value3 = Memory.ReadUInt32(GetMemOperand64(instruction) + 8);
                        uint value4 = Memory.ReadUInt32(GetMemOperand64(instruction) + 12);

                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Vector256.Create(
                                [
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4
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
                        uint value3 = vec[2];
                        uint value4 = vec[3];
                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Vector256.Create(
                                [
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4
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
                        uint value1 = Memory.ReadUInt32(GetMemOperand64(instruction));
                        uint value2 = Memory.ReadUInt32(GetMemOperand64(instruction) + 4);
                        uint value3 = Memory.ReadUInt32(GetMemOperand64(instruction) + 8);
                        uint value4 = Memory.ReadUInt32(GetMemOperand64(instruction) + 12);

                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Vector512.Create(
                                [
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
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
                        uint value3 = vec[2];
                        uint value4 = vec[3];
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Vector512.Create(
                                [
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
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
