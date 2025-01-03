﻿using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void adc(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Adc_AL_imm8:
                {
                    ushort result = (ushort)((ushort)ProcessorRegisters.Al + (byte)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.Al = (byte)result;
                    break;
                }

            case Code.Adc_AX_imm16:
                {
                    uint result = (ushort)((uint)ProcessorRegisters.Ax + (ushort)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.Ax = (ushort)result;
                    break;
                }

            case Code.Adc_EAX_imm32:
                {
                    ulong result = (ulong)((ulong)ProcessorRegisters.Eax + (uint)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.Eax = (uint)result;
                    break;
                }

            case Code.Adc_r16_rm16:
                {
                    uint result = (uint)((uint)ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(0)) + RMEvaluate16(in instruction, 1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)result);
                    break;
                }

            case Code.Adc_r32_rm32:
                {
                    ulong result = (ulong)((ulong)ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(0)) + RMEvaluate32(in instruction, 1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), (uint)result);
                    break;
                }

            case Code.Adc_r64_rm64:
                {
                    Int128 result = (Int128)((Int128)ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(0)) + RMEvaluate64(in instruction, 1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)result);
                    break;
                }

            case Code.Adc_r8_rm8:
                {
                    ushort result = (ushort)((ushort)ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(0)) + RMEvaluate8(in instruction, 1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister8(instruction.GetOpRegister(0), (byte)result);
                    break;
                }

            case Code.Adc_RAX_imm32:
                {
                    Int128 result = (Int128)((Int128)ProcessorRegisters.Rax + (uint)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), (ulong)result);
                    break;
                }

            case Code.Adc_rm16_imm16:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) + (ushort)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)result);
                    break;
                }

            case Code.Adc_rm16_imm8:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) + (byte)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), (ushort)result);
                    break;
                }

            case Code.Adc_rm16_r16:
                {
                    uint result = (uint)((uint)RMEvaluate16(in instruction, 0) + ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1)));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet16(in instruction, (ushort)result);
                    break;
                }

            case Code.Adc_rm32_imm32:
                {
                    ulong result = (ulong)((ulong)RMEvaluate32(in instruction, 0) + (uint)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet32(in instruction, (uint)result);
                    break;
                }

            case Code.Adc_rm32_imm8:
                {
                    ulong result = (ulong)((ulong)RMEvaluate32(in instruction, 0) + (byte)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet32(in instruction, (uint)result);
                    break;
                }

            case Code.Adc_rm32_r32:
                {
                    ulong result = (ulong)((ulong)RMEvaluate32(in instruction, 0) + ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1)));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet32(in instruction, (uint)result);
                    break;
                }

            case Code.Adc_rm64_imm32:
                {
                    Int128 result = (Int128)((Int128)RMEvaluate64(in instruction, 0) + (uint)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet64(in instruction, (ulong)result);
                    break;
                }

            case Code.Adc_rm64_imm8:
                {
                    Int128 result = (Int128)((Int128)RMEvaluate64(in instruction, 0) + (byte)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet64(in instruction, (ulong)result);
                    break;
                }

            case Code.Adc_rm64_r64:
                {
                    Int128 result = (Int128)((Int128)RMEvaluate64(in instruction, 0) + ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1)));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet64(in instruction, (ulong)result);
                    break;
                }

            case Code.Adc_rm8_imm8:
                {
                    ushort result = (ushort)((ushort)RMEvaluate8(in instruction, 0) + (byte)instruction.GetImmediate(1));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet8(in instruction, (byte)result);
                    break;
                }

            case Code.Adc_rm8_r8:
                {
                    ushort result = (ushort)((ushort)RMEvaluate8(in instruction, 0) + ProcessorRegisters.EvaluateRegisterValue8(instruction.GetOpRegister(1)));
                    if (ProcessorRegisters.RFlagsCF) result++;
                    ProcessFlags(result);
                    RMSet8(in instruction, (byte)result);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
