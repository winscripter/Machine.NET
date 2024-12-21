using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpbroadcastq(in Instruction instruction)
    {
        switch(instruction.Code)
        {
            case Code.EVEX_Vpbroadcastq_xmm_k1z_r64:
                {
                    byte value = (byte)ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));
                    Vector128<byte> vec = Vector128.Create<byte>(value);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastq_xmm_k1z_xmmm64:
                {
                    byte value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand64(in instruction)] : ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, byte>()[0];
                    Vector128<byte> vec = Vector128.Create<byte>(value);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastq_ymm_k1z_r64:
                {
                    byte value = (byte)ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));
                    Vector256<byte> vec = Vector256.Create<byte>(value);
                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), vec.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastq_ymm_k1z_xmmm64:
                {
                    byte value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand64(in instruction)] : ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, byte>()[0];
                    Vector256<byte> vec = Vector256.Create<byte>(value);
                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), vec.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastq_zmm_k1z_r64:
                {
                    byte value = (byte)ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));
                    Vector512<byte> vec = Vector512.Create<byte>(value);
                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), vec.As<byte, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastq_zmm_k1z_xmmm64:
                {
                    byte value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand64(in instruction)] : ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, byte>()[0];
                    Vector512<byte> vec = Vector512.Create<byte>(value);
                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), vec.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
            }
        }
}
