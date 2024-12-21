using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pandn(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pandn_mm_mmm64:
                {
                    Vector64<ulong> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Vector64.Create(this.Memory.ReadUInt64(GetMemOperand(in instruction))),
                        OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))),
                        _ => Vector64<ulong>.Zero
                    };
                    Vector64<ulong> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0)));
                    Vector64<ulong> result = destVector;

                    for (int i = 0; i < Vector64<ulong>.Count; i++)
                    {
                        result = result.WithElement(i, inputVector[i] & ~destVector[i]);
                    }

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.ToScalar());
                    break;
                }

            case Code.Pandn_xmm_xmmm128:
                {
                    Vector128<ulong> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand(in instruction)).As<float, ulong>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>(),
                        _ => Vector128<ulong>.Zero
                    };
                    Vector128<ulong> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    Vector128<ulong> result = destVector;

                    for (int i = 0; i < Vector64<ulong>.Count; i++)
                    {
                        result = result.WithElement(i, inputVector[i] & ~destVector[i]);
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
