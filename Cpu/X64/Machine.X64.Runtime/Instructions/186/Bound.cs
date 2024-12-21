using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void bound(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Bound_r16_m1616:
                {
                    ulong operand = GetMemOperand(in instruction);
                    ushort lowerBound = this.Memory.ReadUInt16(operand);
                    ushort upperBound = this.Memory.ReadUInt16(operand + 2);
                    ushort indexRegister = this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(0));

                    if (indexRegister < lowerBound || indexRegister > upperBound)
                        this.RaiseUnboundIndex();

                    break;
                }

            case Code.Bound_r32_m3232:
                {
                    ulong operand = GetMemOperand(in instruction);
                    uint lowerBound = this.Memory.ReadUInt32(operand);
                    uint upperBound = this.Memory.ReadUInt32(operand + 2);
                    uint indexRegister = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));

                    if (indexRegister < lowerBound || indexRegister > upperBound)
                        this.RaiseUnboundIndex();

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
