﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void xorpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Xorpd_xmm_xmmm128:
                {
                    Register destination = instruction.GetOpRegister(0);
                    Vector128<double> dest = ProcessorRegisters.EvaluateXmm(destination).As<float, double>();
                    Vector128<double> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, double>();
                    Vector128<double> result = dest ^ src; // XOR on vector XORs everything inside
                    ProcessorRegisters.SetXmm(destination, result.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
