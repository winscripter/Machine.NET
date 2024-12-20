﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movlpd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movlpd_m64_xmm:
                {
                    Vector128<double> xmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>();
                    Vector64<double> low = xmm.GetLower();
                    this.Memory.WriteBinaryVector64(GetMemOperand64(instruction), low);
                    break;
                }

            case Code.Movlpd_xmm_m64:
                {
                    Vector64<double> vec = this.Memory.ReadBinaryVector64(GetMemOperand64(instruction)).As<float, double>();
                    Vector128<double> fetchedXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, double>();
                    fetchedXmm = fetchedXmm.WithLower(vec);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), fetchedXmm.As<double, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}