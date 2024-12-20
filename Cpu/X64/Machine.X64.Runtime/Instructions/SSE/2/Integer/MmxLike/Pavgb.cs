﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pavgb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pavgb_mm_mmm64:
                {
                    Vector64<byte> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, byte>();
                    Vector64<byte> dst = GetVectorFromMM(in instruction, 0).As<ulong, byte>();
                    Vector64<byte> result = Vector64<byte>.Zero;

                    for (int i = 0; i < Vector64<byte>.Count; i++)
                    {
                        result = result.WithElement(i, (byte)((dst[i] + src[i] + 1) >> 1));
                    }

                    WriteVector64ToMM(in instruction, 0, result);
                    break;
                }

            case Code.Pavgb_xmm_xmmm128:
                {
                    Vector128<byte> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>();
                    Vector128<byte> result = Vector128<byte>.Zero;

                    for (int i = 0; i < Vector128<byte>.Count; i++)
                    {
                        result = result.WithElement(i, (byte)((dst[i] + src[i] + 1) >> 1));
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<byte, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
