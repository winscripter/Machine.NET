using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;
public partial class CpuRuntime
{
    private void vbroadcasti64x2(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcasti64x2_ymm_k1z_m128:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong value1 = Memory.ReadUInt64(GetMemOperand(in instruction));
                        ulong value2 = Memory.ReadUInt64(GetMemOperand(in instruction) + 8);

                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Vector256.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<ulong, float>()
                        );
                    }
                    else
                    {
                        Vector256<ulong> vec = ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, ulong>();
                        ulong value1 = vec[0];
                        ulong value2 = vec[1];

                        ProcessorRegisters.SetYmm(
                            instruction.GetOpRegister(0),
                            Vector256.Create(
                                [
                                    value1,
                                    value2,
                                    value1,
                                    value2
                                ]
                            )
                            .As<ulong, float>()
                        );
                    }
                    break;
                }

            case Code.EVEX_Vbroadcasti32x2_zmm_k1z_xmmm64:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong value1 = Memory.ReadUInt64(GetMemOperand(in instruction));
                        ulong value2 = Memory.ReadUInt64(GetMemOperand(in instruction) + 8);

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
                                    value2
                                ]
                            )
                            .As<ulong, float>()
                        );
                    }
                    else
                    {
                        Vector512<ulong> vec = ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, ulong>();
                        ulong value1 = vec[0];
                        ulong value2 = vec[1];

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
                                    value2
                                ]
                            )
                            .As<ulong, float>()
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
