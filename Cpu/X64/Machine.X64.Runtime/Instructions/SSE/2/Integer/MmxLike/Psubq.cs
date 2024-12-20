using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psubq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psubq_mm_mmm64:
                {
                    Vector64<ulong> inputVector = this.EvaluateMMOrMemoryAsVector64(in instruction, 1).AsUInt64();
                    Vector64<ulong> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, ulong>();
                    Vector64<ulong> result = inputVector - destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<ulong, ulong>().ToScalar());
                    break;
                }

            case Code.Psubq_xmm_xmmm128:
                {
                    Vector128<ulong> inputVector = EvaluateXmmFromInstruction(in instruction, 1).AsUInt64();
                    Vector128<ulong> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    Vector128<ulong> result = inputVector - destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
