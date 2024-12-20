using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Collections;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pmovmskb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pmovmskb_r32_mm:
                {
                    uint result = 0;
                    byte[] bytes = BitConverter.GetBytes(result);
                    byte msbResult = 0;

                    for (int i = 0; i < bytes.Length && i < 8; i++) // only process up to 8 bytes
                    {
                        msbResult |= (byte)((bytes[i] & 0b10000000) >> 7 << (7 - i));
                    }

                    uint r32 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0));
                    BitUtilities.SetLower8Bits(ref r32, msbResult);
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), r32);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
