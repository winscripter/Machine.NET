﻿using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pcmpestrm(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pcmpestrm_xmm_xmmm128_imm8:
                {
                    byte imm8 = (byte)instruction.GetImmediate(2);
                    bool useWordComparison = imm8 is >= 8 and <= 15;

                    Vector128<float> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0));
                    Vector128<float> src = EvaluateXmmFromInstruction(in instruction, 1);

                    if (useWordComparison)
                    {
                        this.ProcessorRegisters.Ecx = (uint)ByteCore(dst.AsByte(), src.AsByte());
                    }
                    else
                    {
                        this.ProcessorRegisters.Ecx = (uint)WordCore(dst.AsUInt16(), src.AsUInt16());
                    }

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }

        static int ByteCore(Vector128<byte> x, Vector128<byte> y)
        {
            int bitmask = 0;
            for (int i = 0; i < Vector128<byte>.Count; i++)
            {
                if (x[i] != y[i])
                {
                    bitmask |= 1 << i;
                }
            }
            return bitmask;
        }

        static int WordCore(Vector128<ushort> x, Vector128<ushort> y)
        {
            int bitmask = 0;
            for (int i = 0; i < Vector128<ushort>.Count; i++)
            {
                if (x[i] != y[i])
                {
                    bitmask |= 1 << i;
                }
            }
            return bitmask;
        }
    }
}
