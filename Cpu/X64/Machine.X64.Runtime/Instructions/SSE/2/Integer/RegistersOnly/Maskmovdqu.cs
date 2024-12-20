using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void maskmovdqu(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Maskmovdqu_rDI_xmm_xmm:
                {
                    Register destinationRegister = instruction.GetOpRegister(0);

                    Vector128<byte> xmm1 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, byte>();
                    Vector128<byte> xmm2 = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(2)).As<float, byte>();

                    ulong baseAddress = destinationRegister switch
                    {
                        Register.DI => this.ProcessorRegisters.Di,
                        Register.ESI => this.ProcessorRegisters.Esi,
                        Register.RSI => this.ProcessorRegisters.Rsi,
                        _ => 0uL
                    };

                    for (int i = 0; i < Vector128<byte>.Count; i++)
                    {
                        byte mask = xmm2[i];
                        if ((mask & 0x80) == 0)
                        {
                            // In conditions where mask & 0x80 does not result in 0,
                            // the item in the address is either kept as zero or what
                            // it used to be prior to the invocation of the instruction.
                            this.Memory[baseAddress + (ulong)i] = xmm1[i];
                        }
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
