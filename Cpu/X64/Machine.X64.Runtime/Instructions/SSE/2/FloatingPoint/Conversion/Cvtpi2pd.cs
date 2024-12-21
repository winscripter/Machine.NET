using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtpi2pd(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtpi2pd_xmm_mmm64:
                {
                    (uint a, uint b) = (0u, 0u);
                    switch (instruction.GetOpKind(1))
                    {
                        case OpKind.Register:
                            {
                                ulong mmValue = this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(1));
                                (a, b) = (BitUtilities.GetLower32Bits(mmValue), BitUtilities.GetUpper32Bits(mmValue));
                                break;
                            }

                        case OpKind.Memory:
                            {
                                ulong memoryValue = this.Memory.ReadUInt64(GetMemOperand(in instruction));
                                (a, b) = (BitUtilities.GetLower32Bits(memoryValue), BitUtilities.GetUpper32Bits(memoryValue));
                                break;
                            }
                    }

                    Vector128<double> result = Vector128.Create((double)a, b);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<double, float>());

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
