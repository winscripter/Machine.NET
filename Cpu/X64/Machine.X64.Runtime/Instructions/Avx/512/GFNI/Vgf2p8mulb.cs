using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vgf2p8mulb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vgf2p8mulb_xmm_k1z_xmm_xmmm128:
                {
                    Vector128<byte> input1 = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> input2 = EvaluateXmmFromInstruction(in instruction, 2).As<float, byte>().InverseAll();

                    Vector128<byte> result = Vector128<byte>.Zero;
                    for (int i = 0; i < Vector128<byte>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            GaloisFieldTransform.Multiply(a: input1[i], b: input2[i]));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vgf2p8mulb_ymm_k1z_ymm_ymmm256:
                {
                    Vector256<byte> input1 = EvaluateYmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector256<byte> input2 = EvaluateYmmFromInstruction(in instruction, 2).As<float, byte>().InverseAll();

                    Vector256<byte> result = Vector256<byte>.Zero;
                    for (int i = 0; i < Vector256<byte>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            GaloisFieldTransform.Multiply(a: input1[i], b: input2[i]));
                    }

                    this.ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vgf2p8mulb_zmm_k1z_zmm_zmmm512:
                {
                    Vector512<byte> input1 = EvaluateZmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector512<byte> input2 = EvaluateZmmFromInstruction(in instruction, 2).As<float, byte>().InverseAll();

                    Vector512<byte> result = Vector512<byte>.Zero;
                    for (int i = 0; i < Vector512<byte>.Count; i++)
                    {
                        result = result.WithElement(
                            i,
                            GaloisFieldTransform.Multiply(a: input1[i], b: input2[i]));
                    }

                    this.ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
