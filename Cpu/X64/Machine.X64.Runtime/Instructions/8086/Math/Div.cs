using Iced.Intel;
using Machine.X64.Runtime.Errors;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void div(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Div_rm8:
                {
                    byte rm8 = RMEvaluate8(in instruction, 0);
                    if (rm8 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }
                    byte quotient = (byte)(this.ProcessorRegisters.Al / rm8);
                    byte remainder = (byte)(this.ProcessorRegisters.Al % rm8);

                    this.ProcessorRegisters.Al = quotient;
                    this.ProcessorRegisters.Ah = remainder;

                    break;
                }

            case Code.Div_rm16:
                {
                    ushort rm16 = RMEvaluate16(in instruction, 0);
                    if (rm16 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }

                    ushort quotient = (ushort)(this.ProcessorRegisters.Ax / rm16);
                    ushort remainder = (ushort)(this.ProcessorRegisters.Ax % rm16);

                    this.ProcessorRegisters.Ax = quotient;
                    RMSet16(in instruction, remainder, 0);

                    break;
                }

             case Code.Div_rm32:
                {
                    uint rm32 = RMEvaluate32(in instruction, 0);
                    if (rm32 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }

                    uint quotient = (uint)(this.ProcessorRegisters.Eax / rm32);
                    uint remainder = (uint)(this.ProcessorRegisters.Eax % rm32);

                    this.ProcessorRegisters.Eax = quotient;
                    RMSet32(in instruction, remainder, 0);

                    break;
                }

            case Code.Div_rm64:
                {
                    ulong rm64 = RMEvaluate64(in instruction, 0);
                    if (rm64 == 0)
                    {
                        RaiseException(StaticErrors.ZeroDivision);
                        break;
                    }

                    ulong quotient = (ulong)(this.ProcessorRegisters.Rax / rm64);
                    ulong remainder = (ulong)(this.ProcessorRegisters.Rax % rm64);

                    this.ProcessorRegisters.Rax = quotient;
                    RMSet64(in instruction, remainder, 0);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
