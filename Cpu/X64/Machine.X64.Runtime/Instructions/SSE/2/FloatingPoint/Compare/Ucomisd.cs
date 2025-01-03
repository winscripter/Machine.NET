﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void ucomisd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Ucomisd_xmm_xmmm64:
                {
                    double valueToCompareFrom = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>().ToScalar();
                    double valueToCompareWith = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadDouble(GetMemOperand(in instruction)),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar(),
                        _ => 0
                    };
                    this.ProcessorRegisters.RFlagsZF = valueToCompareFrom == valueToCompareWith;
                    this.ProcessorRegisters.RFlagsPF = double.IsNaN(valueToCompareFrom) || double.IsNaN(valueToCompareWith);
                    this.ProcessorRegisters.RFlagsCF = valueToCompareFrom < valueToCompareWith;

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
