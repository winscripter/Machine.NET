using Iced.Intel;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vzeroall(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Vzeroall:
                {
                    this.ProcessorRegisters.Ymm0 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm1 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm2 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm3 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm4 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm5 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm6 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm7 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm8 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm9 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm10 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm11 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm12 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm13 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm14 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm15 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm16 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm17 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm18 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm19 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm20 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm21 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm22 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm23 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm24 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm25 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm26 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm27 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm28 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm29 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm30 = Vector256<float>.Zero;
                    this.ProcessorRegisters.Ymm31 = Vector256<float>.Zero;
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
