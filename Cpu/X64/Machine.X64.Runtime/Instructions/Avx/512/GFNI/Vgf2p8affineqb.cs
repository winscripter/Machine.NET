using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vgf2p8affineqb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vgf2p8affineqb_xmm_k1z_xmm_xmmm128b64_imm8:
                {
                    Vector128<byte> input = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> matrix = EvaluateXmmFromInstruction(in instruction, 2).As<float, byte>();
                    byte constant = (byte)instruction.GetImmediate(3);

                    Vector128<byte> result = Vector128<byte>.Zero;
                    for (int i = 0; i < Vector128<byte>.Count; i++)
                        result = result.WithElement(i, GaloisFieldTransform.AffineTransform(
                            x: input[i],
                            matrix: matrix,
                            constant: constant));

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vgf2p8affineqb_ymm_k1z_ymm_ymmm256b64_imm8:
                {
                    Vector256<byte> input = EvaluateYmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector256<byte> matrix = EvaluateYmmFromInstruction(in instruction, 2).As<float, byte>();
                    byte constant = (byte)instruction.GetImmediate(3);

                    Vector256<byte> result = Vector256<byte>.Zero;
                    for (int i = 0; i < Vector256<byte>.Count; i++)
                        result = result.WithElement(i, GaloisFieldTransform.AffineTransform(
                            x: input[i],
                            matrix: matrix,
                            constant: constant));

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vgf2p8affineqb_zmm_k1z_zmm_zmmm512b64_imm8:
                {
                    Vector512<byte> input = EvaluateZmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector512<byte> matrix = EvaluateZmmFromInstruction(in instruction, 2).As<float, byte>();
                    byte constant = (byte)instruction.GetImmediate(3);

                    Vector512<byte> result = Vector512<byte>.Zero;
                    for (int i = 0; i < Vector512<byte>.Count; i++)
                        result = result.WithElement(i, GaloisFieldTransform.AffineTransform(
                            x: input[i],
                            matrix: matrix,
                            constant: constant));

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
