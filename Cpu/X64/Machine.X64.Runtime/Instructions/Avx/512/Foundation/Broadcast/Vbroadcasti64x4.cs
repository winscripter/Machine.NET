using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vbroadcasti64x4(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vbroadcasti64x4_zmm_k1z_m256:
                {
                    if (instruction.GetOpKind(1) == OpKind.Memory)
                    {
                        ulong value1 = Memory.ReadUInt64(GetMemOperand64(in instruction));
                        ulong value2 = Memory.ReadUInt64(GetMemOperand64(in instruction) + 8);
                        ulong value3 = Memory.ReadUInt64(GetMemOperand64(in instruction) + 16);
                        ulong value4 = Memory.ReadUInt64(GetMemOperand64(in instruction) + 24);

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
                        ulong value3 = vec[2];
                        ulong value4 = vec[3];

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
