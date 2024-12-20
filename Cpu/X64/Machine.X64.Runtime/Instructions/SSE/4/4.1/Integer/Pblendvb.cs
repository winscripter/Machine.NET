using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pblendvb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pblendvb_xmm_xmmm128:
                {
                    Vector128<byte> a = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>();
                    Vector128<byte> b = EvaluateXmmFromInstruction(in instruction, 1).As<float, byte>();
                    Vector128<byte> xmm0 = this.ProcessorRegisters.Xmm0.As<float, byte>();

                    Vector128<byte> result = Vector128<byte>.Zero;
                    for (int i = 0; i < 16; i++)
                    {
                        result = result.WithElement(i, (xmm0.GetElement(i) & 0x80) != 0 ? b.GetElement(i) : a.GetElement(i));
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
