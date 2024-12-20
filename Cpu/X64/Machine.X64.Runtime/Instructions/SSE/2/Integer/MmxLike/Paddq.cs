﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void paddq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Paddq_mm_mmm64:
                {
                    Vector64<ulong> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => Vector64.Create(this.Memory.ReadUInt64(GetMemOperand64(instruction))).As<ulong, ulong>(),
                        OpKind.Register => Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1))).As<ulong, ulong>(),
                        _ => Vector64<ulong>.Zero
                    };
                    Vector64<ulong> destVector = Vector64.Create(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, ulong>();
                    Vector64<ulong> result = inputVector + destVector;
                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<ulong, ulong>().ToScalar());
                    break;
                }

            case Code.Paddq_xmm_xmmm128:
                {
                    Vector128<ulong> inputVector = instruction.GetOpKind(1) switch
                    {
                        OpKind.Memory => this.Memory.ReadBinaryVector128(GetMemOperand64(instruction)).As<float, ulong>(),
                        OpKind.Register => this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, ulong>(),
                        _ => Vector128<ulong>.Zero
                    };
                    Vector128<ulong> destVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ulong>();
                    Vector128<ulong> result = inputVector + destVector;
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ulong, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}