using Iced.Intel;
using Machine.X64.Runtime.Errors;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void idiv(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Idiv_rm8:
                {
                    sbyte rm8 = (sbyte)RMEvaluate8(in instruction, 0);
                    if (rm8 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }
                    sbyte quotient = (sbyte)((sbyte)this.ProcessorRegisters.Al / rm8);
                    sbyte remainder = (sbyte)((sbyte)this.ProcessorRegisters.Al % rm8);

                    this.ProcessorRegisters.Al = (byte)quotient;
                    this.ProcessorRegisters.Ah = (byte)remainder;

                    break;
                }

            case Code.Idiv_rm16:
                {
                    short rm16 = (short)RMEvaluate16(in instruction, 0);
                    if (rm16 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }

                    short quotient = (short)((short)this.ProcessorRegisters.Ax / rm16);
                    short remainder = (short)((short)this.ProcessorRegisters.Ax % rm16);

                    this.ProcessorRegisters.Ax = (ushort)quotient;
                    RMSet16(in instruction, (ushort)remainder, 0);

                    break;
                }

            case Code.Idiv_rm32:
                {
                    int rm32 = (int)RMEvaluate32(in instruction, 0);
                    if (rm32 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }

                    int quotient = (int)((int)this.ProcessorRegisters.Eax / rm32);
                    int remainder = (int)((int)this.ProcessorRegisters.Eax % rm32);

                    this.ProcessorRegisters.Eax = (uint)quotient;
                    RMSet32(in instruction, (uint)remainder, 0);

                    break;
                }

            case Code.Idiv_rm64:
                {
                    long rm64 = (long)RMEvaluate64(in instruction, 0);
                    if (rm64 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }

                    long quotient = (long)((long)this.ProcessorRegisters.Rax / rm64);
                    long remainder = (long)((long)this.ProcessorRegisters.Rax % rm64);

                    this.ProcessorRegisters.Rax = (ulong)quotient;
                    RMSet64(in instruction, (ulong)remainder, 0);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
