﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void psignw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Psignw_mm_mmm64:
                {
                    Vector64<short> src = GetVectorFromMMOrMemory64(in instruction, 1).As<ulong, short>();
                    Vector64<short> dst = GetVectorFromMM(in instruction, 0).As<ulong, short>();

                    Vector64<short> result = Vector64<short>.Zero;
                    for (int i = 0; i < Vector64<short>.Count; i++)
                    {
                        result = result.WithElement(i, dst[i] < 0 ? (short)-src[i] : src[i]);
                    }

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), result.As<short, ulong>()[0]);
                    break;
                }

            case Code.Psignw_xmm_xmmm128:
                {
                    Vector128<short> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, short>();
                    Vector128<short> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, short>();

                    Vector128<short> result = Vector128<short>.Zero;
                    for (int i = 0; i < Vector128<short>.Count; i++)
                    {
                        result = result.WithElement(i, dst[i] < 0 ? (short)-src[i] : src[i]);
                    }

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<short, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
