using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movq_mmm64_mm:
                {
                    ulong mm = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0));
                    switch (instruction.GetOpKind(1))
                    {
                        case OpKind.Memory:
                            this.Memory.WriteUInt64(GetMemOperand64(in instruction), mm);
                            break;

                        case OpKind.Register:
                            this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mm);
                            break;
                    }
                    break;
                }

            case Code.Movq_mm_mmm64:
                {
                    ulong mmm64 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadUInt64(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1)),
                        _ => 0uL
                    };
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mmm64);
                    break;
                }

            case Code.Movq_mm_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), rm64);
                    break;
                }

            case Code.Movq_rm64_mm:
                {
                    ulong mm = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1));
                    RMSet64(in instruction, mm, 0);
                    break;
                }

            case Code.Movq_rm64_xmm:
                {
                    ulong xmmScalar = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>().ToScalar();
                    RMSet64(in instruction, xmmScalar, 0);
                    break;
                }

            case Code.Movq_xmmm64_xmm:
                {
                    Vector128<ulong> xmmAsQword = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    OpKind opKind = instruction.GetOpKind(0);

                    switch (opKind)
                    {
                        case OpKind.Memory:
                            {
                                this.Memory.WriteUInt64(GetMemOperand64(in instruction), xmmAsQword.ToScalar());
                                break;
                            }

                        case OpKind.Register:
                            {
                                Vector128<ulong> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                                xmm = xmm.WithElement(0, xmmAsQword.ToScalar());
                                this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), xmm.As<ulong, float>());
                                break;
                            }
                    }

                    break;
                }

            case Code.Movq_xmm_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 1);
                    Vector128<ulong> vector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    vector = vector.WithElement(0, rm64);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vector.As<ulong, float>());
                    break;
                }

            case Code.Movq_xmm_xmmm64:
                {
                    ulong rm64 = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadUInt64(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>().ToScalar(),
                        _ => 0uL
                    };

                    Vector128<ulong> vector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    vector = vector.WithElement(0, rm64);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vector.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
