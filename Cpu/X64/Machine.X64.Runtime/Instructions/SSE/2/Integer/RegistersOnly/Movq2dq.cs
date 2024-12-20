using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movq2dq(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movq2dq_xmm_mm:
                {
                    ulong mm = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1));

                    Register destinationRegister = instruction.GetOpRegister(0);
                    Vector128<ulong> destinationVector = this.ProcessorRegisters.EvaluateXmm(destinationRegister).As<float, ulong>();
                    
                    destinationVector = destinationVector.WithElement(0, mm);
                    this.ProcessorRegisters.SetXmm(destinationRegister, destinationVector.As<ulong, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
