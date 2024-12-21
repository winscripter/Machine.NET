using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void vpbroadcastd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.EVEX_Vpbroadcastw_xmm_k1z_r32:
                {
                    uint value = (uint)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    Vector128<uint> vec = Vector128.Create<uint>(value);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastd_xmm_k1z_xmmm32:
                {
                    uint value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand(in instruction)] : ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, uint>()[0];
                    Vector128<uint> vec = Vector128.Create<uint>(value);
                    ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_ymm_k1z_r32:
                {
                    uint value = (uint)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    Vector256<uint> vec = Vector256.Create<uint>(value);
                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastd_ymm_k1z_xmmm32:
                {
                    uint value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand(in instruction)] : ProcessorRegisters.EvaluateYmm(instruction.GetOpRegister(1)).As<float, uint>()[0];
                    Vector256<uint> vec = Vector256.Create<uint>(value);
                    ProcessorRegisters.SetYmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastw_zmm_k1z_r32:
                {
                    uint value = (uint)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    Vector512<uint> vec = Vector512.Create<uint>(value);
                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            case Code.EVEX_Vpbroadcastd_zmm_k1z_xmmm32:
                {
                    uint value = instruction.GetOpKind(1) == OpKind.Memory ? Memory[GetMemOperand(in instruction)] : ProcessorRegisters.EvaluateZmm(instruction.GetOpRegister(1)).As<float, uint>()[0];
                    Vector512<uint> vec = Vector512.Create<uint>(value);
                    ProcessorRegisters.SetZmm(instruction.GetOpRegister(0), vec.As<uint, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
