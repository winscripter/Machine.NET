using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void mpsadbw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Mpsadbw_xmm_xmmm128_imm8:
                {
                    Vector128<byte> dst = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>();
                    Vector128<byte> src = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    byte imm = (byte)instruction.GetImmediate(2);

                    Vector128<ushort> result = Core(dst, src, imm);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }

        static Vector128<ushort> Core(Vector128<byte> a, Vector128<byte> b, byte imm8)
        {
            Vector128<ushort> result = Vector128<ushort>.Zero;
            for (int i = 0; i < 8; i++)
            {
                int sum = 0;
                for (int j = 0; j < 4; j++)
                {
                    int indexA = (imm8 & 0x4) == 0 ? i + j : i + j + 4;
                    int indexB = (imm8 & 0x3) == 0 ? j : j + 4;
                    sum += Math.Abs(a.GetElement(indexA) - b.GetElement(indexB));
                }
                result = result.WithElement(i, (ushort)sum);
            }
            return result;
        }
    }
}
