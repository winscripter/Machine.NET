using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void paddw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Paddw_mm_mmm64:
                {
                    Vector64<ushort> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Vector64.Create(this.Memory.ReadUInt64(GetMemOperand64(instruction))).As<ulong, ushort>(),
                        OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))).As<ulong, ushort>(),
                        _ => Vector64<ushort>.Zero
                    };
                    Vector64<ushort> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, ushort>();
                    Vector64<ushort> result = inputVector + destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<ushort, ulong>().ToScalar());
                    break;
                }

            case Code.Paddw_xmm_xmmm128:
                {
                    Vector128<ushort> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(instruction)).As<float, ushort>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>(),
                        _ => Vector128<ushort>.Zero
                    };
                    Vector128<ushort> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();
                    Vector128<ushort> result = inputVector + destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
