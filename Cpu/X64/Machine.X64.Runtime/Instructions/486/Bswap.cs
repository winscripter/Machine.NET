using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void bswap(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Bswap_r16:
                {
                    // BSWAP on 16-bit register produces an undefined result
                    // without raising #UD.
                    // Just XOR it with a random value. I think 123456789 is cool.

                    this.ProcessorRegisters.WriteToRegister16(
                        instruction.GetOpRegister(0),
                        (ushort)(
                            this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(0)) ^ 123456789u
                        )
                    );
                    break;
                }

            case Code.Bswap_r32:
                {
                    uint r = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));
                    uint result = (r << 24)
                                  | ((r << 8) & 0x00FF0000)
                                  | ((r >> 8) & 0x0000FF00)
                                  | (r >> 24);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), result);
                    break;
                }

            // Not sure how to implement Bswap_r64.

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
