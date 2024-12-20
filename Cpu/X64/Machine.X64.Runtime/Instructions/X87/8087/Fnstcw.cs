﻿using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void fnstcw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Fnstcw_m2byte:
                {
                    this.Memory.WriteUInt16(GetMemOperand(in instruction), this.Fpu.ControlWord);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
