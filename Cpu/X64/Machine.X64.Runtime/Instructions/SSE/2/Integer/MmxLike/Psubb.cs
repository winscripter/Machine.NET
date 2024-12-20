using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psubb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psubb_mm_mmm64:
                {
                    Vector64<byte> inputVector = this.EvaluateMMOrMemoryAsVector64(in instruction, 1).AsByte();
                    Vector64<byte> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, byte>();
                    Vector64<byte> result = inputVector - destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<byte, ulong>().ToScalar());
                    break;
                }

            case Code.Psubb_xmm_xmmm128:
                {
                    Vector128<byte> inputVector = EvaluateXmmFromInstruction(in instruction, 1).AsByte();
                    Vector128<byte> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>();
                    Vector128<byte> result = inputVector - destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
