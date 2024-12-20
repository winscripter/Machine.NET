using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpclmulqdq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpclmulqdq_xmm_xmm_xmmm128_imm8:
                {
                    Vector128<ulong> src1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    Vector128<ulong> src2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, ulong>();
                    Vector128<ulong> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    byte bits0to3 = (byte)(imm8 & 0b00001111);
                    byte bits4to7 = (byte)((imm8 & 0b11110000) >> 4);

                    Vector128<ulong> result = Vector128.Create(
                        bits0to3 % 2 != 0 ? CarrylessMultiply(src1[0], src2[0]) : dst[0],
                        bits4to7 % 2 != 0 ? CarrylessMultiply(src1[1], src2[1]) : dst[1]);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpclmulqdq_ymm_ymm_ymmm256_imm8:
                {
                    Vector256<ulong> src1 = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    Vector256<ulong> src2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, ulong>();
                    Vector256<ulong> dst = this.ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    byte bits0to1 = (byte)(imm8 & 0b00000011);
                    byte bits2to3 = (byte)((imm8 & 0b00001100) >> 2);
                    byte bits4to5 = (byte)((imm8 & 0b00110000) >> 4);
                    byte bits6to7 = (byte)((imm8 & 0b11000000) >> 6);

                    Vector256<ulong> result = Vector256.Create(
                        bits0to1 % 2 != 0 ? CarrylessMultiply(src1[0], src2[0]) : dst[0],
                        bits2to3 % 2 != 0 ? CarrylessMultiply(src1[1], src2[1]) : dst[1],
                        bits4to5 % 2 != 0 ? CarrylessMultiply(src1[2], src2[2]) : dst[2],
                        bits6to7 % 2 != 0 ? CarrylessMultiply(src1[3], src2[3]) : dst[3]);

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            case Code.EVEX_Vpclmulqdq_zmm_zmm_zmmm512_imm8:
                {
                    Vector512<ulong> src1 = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, ulong>();
                    Vector512<ulong> src2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, ulong>();
                    Vector512<ulong> dst = this.ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    byte imm8 = (byte)instruction.GetImmediate(3);

                    Vector512<ulong> result = Vector512.Create(
                        BitUtilities.IsBitSet(imm8, 0) ? CarrylessMultiply(src1[0], src2[0]) : dst[0],
                        BitUtilities.IsBitSet(imm8, 1) ? CarrylessMultiply(src1[1], src2[1]) : dst[1],
                        BitUtilities.IsBitSet(imm8, 2) ? CarrylessMultiply(src1[2], src2[2]) : dst[2],
                        BitUtilities.IsBitSet(imm8, 3) ? CarrylessMultiply(src1[3], src2[3]) : dst[3],
                        BitUtilities.IsBitSet(imm8, 4) ? CarrylessMultiply(src1[4], src2[4]) : dst[4],
                        BitUtilities.IsBitSet(imm8, 5) ? CarrylessMultiply(src1[5], src2[5]) : dst[5],
                        BitUtilities.IsBitSet(imm8, 6) ? CarrylessMultiply(src1[6], src2[6]) : dst[6],
                        BitUtilities.IsBitSet(imm8, 7) ? CarrylessMultiply(src1[7], src2[7]) : dst[7]);

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ulong CarrylessMultiply(ulong x, ulong y)
    {
        ulong result = 0;
        for (int i = 0; i < 64; i++)
        {
            if ((y & (1uL << i)) != 0)
            {
                result ^= x << i;
            }
        }
        return result;
    }
}
