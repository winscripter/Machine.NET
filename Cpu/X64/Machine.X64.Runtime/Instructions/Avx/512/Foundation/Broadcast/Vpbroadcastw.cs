using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpbroadcastw(in Instruction instruction)
    {
        switch(instruction.Code)
        {
            case Code.EVEX_Vpbroadcastw_xmm_k1z_r32:
                {
                    ushort value = (ushort)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    Vector128<ushort> vec = Vector128.Create<ushort>(value);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_xmm_k1z_xmmm16:
                {
                    ushort value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand(in instruction)] : ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ushort>()[0];
                    Vector128<ushort> vec = Vector128.Create<ushort>(value);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_ymm_k1z_r32:
                {
                    ushort value = (ushort)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    Vector256<ushort> vec = Vector256.Create<ushort>(value);
                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), vec.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_ymm_k1z_xmmm16:
                {
                    ushort value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand(in instruction)] : ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, ushort>()[0];
                    Vector256<ushort> vec = Vector256.Create<ushort>(value);
                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), vec.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_zmm_k1z_r32:
                {
                    ushort value = (ushort)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    Vector512<ushort> vec = Vector512.Create<ushort>(value);
                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), vec.As<ushort, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_zmm_k1z_xmmm16:
                {
                    ushort value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand(in instruction)] : ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, ushort>()[0];
                    Vector512<ushort> vec = Vector512.Create<ushort>(value);
                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), vec.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
            }
        }
}
