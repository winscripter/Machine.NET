using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void shufps(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Shufps_xmm_xmmm128_imm8:
                {
                    Vector128<float> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> second = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)),
                        _ => Vector128<float>.Zero
                    };
                    byte imm = (byte)instruction.GetImmediate(2);
                    Vector128<float> result = internal__(first, second, imm);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }

        static Vector128<float> internal__(Vector128<float> first, Vector128<float> second, byte imm)
        {
            float result0 = (imm & 0x03) switch
            {
                0 => first.GetElement(0),
                1 => first.GetElement(1),
                2 => first.GetElement(2),
                3 => first.GetElement(3),
                _ => 0f
            };

            float result1 = ((imm >> 2) & 0x03) switch
            {
                0 => first.GetElement(0),
                1 => first.GetElement(1),
                2 => first.GetElement(2),
                3 => first.GetElement(3),
                _ => 0f
            };

            float result2 = ((imm >> 4) & 0x03) switch
            {
                0 => second.GetElement(0),
                1 => second.GetElement(1),
                2 => second.GetElement(2),
                3 => second.GetElement(3),
                _ => 0f
            };

            float result3 = ((imm >> 6) & 0x03) switch
            {
                0 => second.GetElement(0),
                1 => second.GetElement(1),
                2 => second.GetElement(2),
                3 => second.GetElement(3),
                _ => 0f
            };

            return Vector128.Create(result0, result1, result2, result3);
        }
    }
}
