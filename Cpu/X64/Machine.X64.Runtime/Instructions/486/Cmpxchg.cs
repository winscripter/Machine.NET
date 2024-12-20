using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cmpxchg(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cmpxchg_rm8_r8:
                {
                    byte value1 = RMEvaluate8(in instruction, 0);

                    bool value1IsEqualToAL = this.ProcessorRegisters.Al == value1;

                    if (value1IsEqualToAL)
                    {
                        RMSet8(in instruction, this.ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        this.ProcessorRegisters.Al = value1;
                    }

                    this.ProcessorRegisters.RFlagsZF = value1IsEqualToAL;

                    break;
                }

            case Code.Cmpxchg_rm16_r16:
                {
                    ushort value1 = RMEvaluate16(in instruction, 0);

                    bool value1IsEqualToAX = this.ProcessorRegisters.Ax == value1;

                    if (value1IsEqualToAX)
                    {
                        RMSet16(in instruction, this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        this.ProcessorRegisters.Ax = value1;
                    }

                    this.ProcessorRegisters.RFlagsZF = value1IsEqualToAX;

                    break;
                }

            case Code.Cmpxchg_rm32_r32:
                {
                    uint value1 = RMEvaluate32(in instruction, 0);

                    bool value1IsEqualToEAX = this.ProcessorRegisters.Eax == value1;

                    if (value1IsEqualToEAX)
                    {
                        RMSet32(in instruction, this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        this.ProcessorRegisters.Eax = value1;
                    }

                    this.ProcessorRegisters.RFlagsZF = value1IsEqualToEAX;

                    break;
                }

            case Code.Cmpxchg_rm64_r64:
                {
                    ulong value1 = RMEvaluate64(in instruction, 0);

                    bool value1IsEqualToRAX = this.ProcessorRegisters.Rax == value1;

                    if (value1IsEqualToRAX)
                    {
                        RMSet64(in instruction, this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1)));
                    }
                    else
                    {
                        this.ProcessorRegisters.Rax = value1;
                    }

                    this.ProcessorRegisters.RFlagsZF = value1IsEqualToRAX;

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
