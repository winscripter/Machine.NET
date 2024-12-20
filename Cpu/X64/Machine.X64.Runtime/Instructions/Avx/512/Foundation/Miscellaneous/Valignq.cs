using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void valignq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Valignq_xmm_k1z_xmm_xmmm128b64_imm8:
                {
                    Register src1 = instruction.GetOpRegister(1);
                    Register src2 = instruction.GetOpRegister(2);
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector128<double> result = Align.Vectors(
                        ProcessorRegisters.EvaluateXmm(src1).As<float, double>(),
                        ProcessorRegisters.EvaluateXmm(src2).As<float, double>(),
                        imm8);

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Valignq_ymm_k1z_ymm_ymmm256b64_imm8:
                {
                    Register src1 = instruction.GetOpRegister(1);
                    Register src2 = instruction.GetOpRegister(2);
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector256<double> result = Align.Vectors(
                        ProcessorRegisters.EvaluateYmm(src1).As<float, double>(),
                        ProcessorRegisters.EvaluateYmm(src2).As<float, double>(),
                        imm8);

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            case Code.EVEX_Valignq_zmm_k1z_zmm_zmmm512b64_imm8:
                {
                    Register src1 = instruction.GetOpRegister(1);
                    Register src2 = instruction.GetOpRegister(2);
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector512<double> result = Align.Vectors(
                        ProcessorRegisters.EvaluateZmm(src1).As<float, double>(),
                        ProcessorRegisters.EvaluateZmm(src2).As<float, double>(),
                        imm8);

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
