﻿using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime.Errors;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movdqa(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movdqa_xmmm128_xmm:
                {
                    Vector128<float> inputXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1));
                    OpKind opKind = instruction.GetOpKind(0);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), inputXmm);
                            break;

                        case OpKind.Memory:
                            ulong address = GetMemOperand(in instruction);
                            if (address % 16 == 0)
                            {
                                RaiseException(StaticErrors.GeneralProtectionFault);
                                break;
                            }

                            this.Memory.WriteBinaryVector128(address, inputXmm);
                            break;
                    }
                    break;
                }

            case Code.Movdqa_xmm_xmmm128:
                {
                    OpKind opKind = instruction.GetOpKind(1);
                    switch (opKind)
                    {
                        case OpKind.Register:
                            this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            ulong address = GetMemOperand(in instruction);
                            if (address % 16 == 0)
                            {
                                RaiseException(StaticErrors.GeneralProtectionFault);
                                break;
                            }

                            this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), this.Memory.ReadBinaryVector128(address));
                            break;
                    }
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
