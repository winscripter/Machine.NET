using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void cvtsd2ss(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Cvtsd2ss_xmm_xmmm64:
                {
                    double scalarDoublePrecision = 0D;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Memory:
                            {
                                scalarDoublePrecision = this.Memory.ReadDouble(GetMemOperand64(in instruction));
                                break;
                            }

                        case OpKind.Register:
                            {
                                scalarDoublePrecision = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(1)).As<float, double>().ToScalar();
                                break;
                            }
                    }

                    Vector128<float> resultXmm = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0))
                                                     .WithElement(0, (float)scalarDoublePrecision);
                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), resultXmm);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
