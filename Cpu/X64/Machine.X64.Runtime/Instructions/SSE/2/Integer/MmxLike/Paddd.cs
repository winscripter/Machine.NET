using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void paddd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Paddd_mm_mmm64:
                {
                    Vector64<uint> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Vector64.Create(this.Memory.ReadUInt64(GetMemOperand64(in instruction))).As<ulong, uint>(),
                        OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))).As<ulong, uint>(),
                        _ => Vector64<uint>.Zero
                    };
                    Vector64<uint> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, uint>();
                    Vector64<uint> result = inputVector + destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<uint, ulong>().ToScalar());
                    break;
                }

            case Code.Paddd_xmm_xmmm128:
                {
                    Vector128<uint> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction)).As<float, uint>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, uint>(),
                        _ => Vector128<uint>.Zero
                    };
                    Vector128<uint> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();
                    Vector128<uint> result = inputVector + destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
