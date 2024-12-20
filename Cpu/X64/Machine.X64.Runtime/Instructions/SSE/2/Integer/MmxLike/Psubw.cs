using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psubw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psubw_mm_mmm64:
                {
                    Vector64<ushort> inputVector = this.EvaluateMMOrMemoryAsVector64(in instruction, 1).AsUInt16();
                    Vector64<ushort> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, ushort>();
                    Vector64<ushort> result = inputVector - destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<ushort, ulong>().ToScalar());
                    break;
                }

            case Code.Psubw_xmm_xmmm128:
                {
                    Vector128<ushort> inputVector = EvaluateXmmFromInstruction(in instruction, 1).AsUInt16();
                    Vector128<ushort> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();
                    Vector128<ushort> result = inputVector - destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
