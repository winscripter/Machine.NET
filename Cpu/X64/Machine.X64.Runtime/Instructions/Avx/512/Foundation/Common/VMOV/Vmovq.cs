using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vmovq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vmovq_rm64_xmm:
                {
                    Vector128<ulong> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt64();
                    RMSet64(in instruction, vec.ToScalar(), 0);
                    break;
                }

            case Code.EVEX_Vmovq_xmmm64_xmm:
                {
                    Vector128<ulong> vec = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).AsUInt64();
                    OpKind kind = instruction.GetOpKind(0);
                    switch (kind)
                    {
                        case OpKind.Memory:
                            this.Memory.WriteUInt64(GetMemOperand64(instruction), vec.ToScalar());
                            break;

                        case OpKind.Register:
                            this.AlterScalarOfXmm(instruction.GetOpRegister(0), vec.ToScalar());
                            break;
                    }
                    break;
                }

            case Code.EVEX_Vmovq_xmm_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    AlterScalarOfXmm(instruction.GetOpRegister(0), rm64);
                    break;
                }

            case Code.EVEX_Vmovq_xmm_xmmm64:
                {
                    ulong scalar = ReadXmmScalarOrUInt64(in instruction, 1);
                    AlterScalarOfXmm(instruction.GetOpRegister(0), scalar);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
