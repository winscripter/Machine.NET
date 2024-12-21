using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pand(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pand_mm_mmm64:
                {
                    Vector64<float> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Vector64.Create(this.Memory.ReadUInt64(GetMemOperand(in instruction))).As<ulong, float>(),
                        OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))).As<ulong, float>(),
                        _ => Vector64<float>.Zero
                    };
                    Vector64<float> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, float>();
                    Vector64<float> result = inputVector & destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<float, ulong>().ToScalar());
                    break;
                }

            case Code.Pand_xmm_xmmm128:
                {
                    Vector128<float> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand(in instruction)).As<float, float>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, float>(),
                        _ => Vector128<float>.Zero
                    };
                    Vector128<float> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, float>();
                    Vector128<float> result = inputVector & destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
