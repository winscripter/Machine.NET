using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void por(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Por_mm_mmm64:
                {
                    Vector64<float> inputVector = EvaluateMMOrMemoryAsVector64(in instruction, 1);
                    Vector64<float> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, float>();
                    Vector64<float> result = inputVector | destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<float, ulong>().ToScalar());
                    break;
                }

            case Code.Por_xmm_xmmm128:
                {
                    Vector128<float> inputVector = EvaluateXmmFromInstruction(in instruction, 1);
                    Vector128<float> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, float>();
                    Vector128<float> result = inputVector | destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<float, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
