﻿using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kxorw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.VEX_Kxorw_kr_kr_kr:
                NewOpmaskCore.KXorInstructionMain(ProcessorRegisters, in instruction, instruction.Code, 16);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
