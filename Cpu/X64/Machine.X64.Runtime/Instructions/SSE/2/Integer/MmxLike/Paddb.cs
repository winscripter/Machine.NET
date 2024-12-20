using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void paddb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Paddb_mm_mmm64:
                {
                    Vector64<byte> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Vector64.Create(this.Memory.ReadUInt64(GetMemOperand64(instruction))).As<ulong, byte>(),
                        OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))).As<ulong, byte>(),
                        _ => Vector64<byte>.Zero
                    };
                    Vector64<byte> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, byte>();
                    Vector64<byte> result = inputVector + destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<byte, ulong>().ToScalar());
                    break;
                }

            case Code.Paddb_xmm_xmmm128:
                {
                    Vector128<byte> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(instruction)).As<float, byte>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, byte>(),
                        _ => Vector128<byte>.Zero
                    };
                    Vector128<byte> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>();
                    Vector128<byte> result = inputVector + destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
