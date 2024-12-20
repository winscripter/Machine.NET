﻿using Iced.Intel;
using Machine.X64.Runtime.Core.Avx512;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void kor(in Instruction instruction)
    {
        Code code = instruction.Code;
        switch (code)
        {
            case Code.VEX_Korb_kr_kr_kr:
                NewOpmaskCore.KORInstructionMain(ProcessorRegisters, in instruction, code, 8);
                break;

            case Code.VEX_Kxorw_kr_kr_kr:
                NewOpmaskCore.KORInstructionMain(ProcessorRegisters, in instruction, code, 16);
                break;

            case Code.VEX_Kxord_kr_kr_kr:
                NewOpmaskCore.KORInstructionMain(ProcessorRegisters, in instruction, code, 32);
                break;

            case Code.VEX_Kxorq_kr_kr_kr:
                NewOpmaskCore.KORInstructionMain(ProcessorRegisters, in instruction, code, 64);
                break;

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }

    private void korb(in Instruction instruction) => kadd(in instruction);

    private void korw(in Instruction instruction) => kadd(in instruction);

    private void kord(in Instruction instruction) => kadd(in instruction);

    private void korq(in Instruction instruction) => kadd(in instruction);
}