using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movdir64b(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movdir64b_r16_m512:
                {
                    ulong targetAddress = (ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(0));
                    Vector512<float> vector = this.Memory.ReadBinaryVector512(GetMemOperand(in instruction));
                    this.Memory.WriteBinaryVector512(targetAddress, vector);
                    break;
                }

            case Code.Movdir64b_r32_m512:
                {
                    ulong targetAddress = (ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));
                    Vector512<float> vector = this.Memory.ReadBinaryVector512(GetMemOperand(in instruction));
                    this.Memory.WriteBinaryVector512(targetAddress, vector);
                    break;
                }

            case Code.Movdir64b_r64_m512:
                {
                    ulong targetAddress = (ulong)(this.ProcessorRegisters.Es << 4) + this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0));
                    Vector512<float> vector = this.Memory.ReadBinaryVector512(GetMemOperand(in instruction));
                    this.Memory.WriteBinaryVector512(targetAddress, vector);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
