using Iced.Intel;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vzeroupper(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Vzeroupper:
                {
                    this.ProcessorRegisters.Ymm0 = this.ProcessorRegisters.Ymm0.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm1 = this.ProcessorRegisters.Ymm1.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm2 = this.ProcessorRegisters.Ymm2.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm3 = this.ProcessorRegisters.Ymm3.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm4 = this.ProcessorRegisters.Ymm4.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm5 = this.ProcessorRegisters.Ymm5.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm6 = this.ProcessorRegisters.Ymm6.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm7 = this.ProcessorRegisters.Ymm7.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm8 = this.ProcessorRegisters.Ymm8.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm9 = this.ProcessorRegisters.Ymm9.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm10 = this.ProcessorRegisters.Ymm10.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm11 = this.ProcessorRegisters.Ymm11.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm12 = this.ProcessorRegisters.Ymm12.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm13 = this.ProcessorRegisters.Ymm13.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm14 = this.ProcessorRegisters.Ymm14.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm15 = this.ProcessorRegisters.Ymm15.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm16 = this.ProcessorRegisters.Ymm16.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm17 = this.ProcessorRegisters.Ymm17.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm18 = this.ProcessorRegisters.Ymm18.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm19 = this.ProcessorRegisters.Ymm19.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm20 = this.ProcessorRegisters.Ymm20.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm21 = this.ProcessorRegisters.Ymm21.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm22 = this.ProcessorRegisters.Ymm22.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm23 = this.ProcessorRegisters.Ymm23.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm24 = this.ProcessorRegisters.Ymm24.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm25 = this.ProcessorRegisters.Ymm25.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm26 = this.ProcessorRegisters.Ymm26.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm27 = this.ProcessorRegisters.Ymm27.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm28 = this.ProcessorRegisters.Ymm28.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm29 = this.ProcessorRegisters.Ymm29.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm30 = this.ProcessorRegisters.Ymm30.WithUpper(Vector128<float>.Zero);
                    this.ProcessorRegisters.Ymm31 = this.ProcessorRegisters.Ymm31.WithUpper(Vector128<float>.Zero);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
