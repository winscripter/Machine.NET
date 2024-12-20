using Iced.Intel;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void phminposuw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Phminposuw_xmm_xmmm128:
                {
                    Vector128<ushort> vector = EvaluateXmmFromInstruction(in instruction, 1).As<float, ushort>();
                    ushort minValue = ushort.MaxValue;
                    int minPos = -1;

                    for (int i = 0; i < Vector128<ushort>.Count; i++)
                    {
                        ushort value = vector.GetElement(i);
                        if (value < minValue)
                        {
                            minValue = value;
                            minPos = i;
                        }
                    }

                    Vector128<ushort> result = Vector128<ushort>.Zero;
                    result = result.WithElement(0, minValue);
                    result = result.WithElement(1, (ushort)minPos);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), result.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
