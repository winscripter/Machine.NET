using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psubd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psubd_mm_mmm64:
                {
                    Vector64<uint> inputVector = this.EvaluateMMOrMemoryAsVector64(in instruction, 1).AsUInt32();
                    Vector64<uint> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, uint>();
                    Vector64<uint> result = inputVector - destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<uint, ulong>().ToScalar());
                    break;
                }

            case Code.Psubd_xmm_xmmm128:
                {
                    Vector128<uint> inputVector = EvaluateXmmFromInstruction(in instruction, 1).AsUInt32();
                    Vector128<uint> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, uint>();
                    Vector128<uint> result = inputVector - destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
