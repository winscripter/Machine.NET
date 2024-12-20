using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Core.Avx512;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void valignd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Valignd_xmm_k1z_xmm_xmmm128b32_imm8:
                {
                    Register src1 = instruction.GetOpRegister(1);
                    Register src2 = instruction.GetOpRegister(2);
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector128<float> result = Align.Vectors(
                        ProcessorRegisters.EvaluateXmm(src1),
                        ProcessorRegisters.EvaluateXmm(src2),
                        imm8);

                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Valignd_ymm_k1z_ymm_ymmm256b32_imm8:
                {
                    Register src1 = instruction.GetOpRegister(1);
                    Register src2 = instruction.GetOpRegister(2);
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector256<float> result = Align.Vectors(
                        ProcessorRegisters.EvaluateYmm(src1),
                        ProcessorRegisters.EvaluateYmm(src2),
                        imm8);

                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result);
                    break;
                }

            case Code.EVEX_Valignd_zmm_k1z_zmm_zmmm512b32_imm8:
                {
                    Register src1 = instruction.GetOpRegister(1);
                    Register src2 = instruction.GetOpRegister(2);
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector512<float> result = Align.Vectors(
                        ProcessorRegisters.EvaluateZmm(src1),
                        ProcessorRegisters.EvaluateZmm(src2),
                        imm8);

                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
