using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;
public partial class CpuRuntime
{
    private void vbroadcasti32x8(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcasti32x2_zmm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        uint value1 = Memory.ReadUInt32(GetMemOperand64(in instruction));
                        uint value2 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 4);
                        uint value3 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 8);
                        uint value4 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 12);
                        uint value5 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 16);
                        uint value6 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 20);
                        uint value7 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 24);
                        uint value8 = Memory.ReadUInt32(GetMemOperand64(in instruction) + 28);

                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Vector512.Create(
                                [
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value5,
                                    value6,
                                    value7,
                                    value8,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value5,
                                    value6,
                                    value7,
                                    value8,
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
                        uint value5 = vec[4];
                        uint value6 = vec[5];
                        uint value7 = vec[6];
                        uint value8 = vec[7];
                        ProcessorRegisters.SetZmm(
                            instruction.GetOpRegister(0),
                            Vector512.Create(
                                [
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value5,
                                    value6,
                                    value7,
                                    value8,
                                    value1,
                                    value2,
                                    value3,
                                    value4,
                                    value5,
                                    value6,
                                    value7,
                                    value8,
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
