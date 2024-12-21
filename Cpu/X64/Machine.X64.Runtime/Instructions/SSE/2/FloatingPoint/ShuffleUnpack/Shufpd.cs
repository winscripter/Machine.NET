using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void shufpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Shufpd_xmm_xmmm128_imm8:
                {
                    Vector128<double> first = ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    Vector128<double> second = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(in instruction)).As<float, double>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>(),
                        _ => Vector128<double>.Zero
                    };
                    byte imm = (byte)instruction.GetImmediate(2);
                    Vector128<double> result = internal__(first, second, imm);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }

        static Vector128<double> internal__(Vector128<double> first, Vector128<double> second, byte imm)
        {
            double result0 = (imm & 0x03) switch
            {
                0 => first.GetElement(0),
                1 => first.GetElement(1),
                2 => first.GetElement(2),
                3 => first.GetElement(3),
                _ => 0d
            };

            double result1 = ((imm >> 2) & 0x03) switch
            {
                0 => first.GetElement(0),
                1 => first.GetElement(1),
                2 => first.GetElement(2),
                3 => first.GetElement(3),
                _ => 0d
            };

            return Vector128.Create(result0, result1);
        }
    }
}
