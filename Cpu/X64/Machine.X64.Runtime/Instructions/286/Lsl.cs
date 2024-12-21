using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void lsl(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Lsl_r16_rm16:
                {
                    ushort segmentSelector = RMEvaluate16(in instruction, 1);
                    this.ProcessorRegisters.WriteToRegister16(
                        instruction.GetOpRegister(0),
                        (ushort)SegmentDescriptor.FromBase(
                            this.Memory.GetDescriptorAtSegment(segmentSelector)
                        ).SegmentLimit
                    );
                    break;
                }

            case Code.Lsl_r32_r32m16:
                {
                    uint segmentSelector = instruction.GetOpKind(1) == OpKind.Register
                        ? this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1))
                        : RMEvaluate16(in instruction, 1);

                    this.ProcessorRegisters.WriteToRegister32(
                        instruction.GetOpRegister(0),
                        SegmentDescriptor.FromBase(
                            this.Memory.GetDescriptorAtSegment(segmentSelector)
                        ).SegmentLimit
                    );
                    break;
                }

            case Code.Lsl_r64_r64m16:
                {
                    ulong segmentSelector = instruction.GetOpKind(1) == OpKind.Register
                        ? this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1))
                        : RMEvaluate16(in instruction, 1);

                    this.ProcessorRegisters.WriteToRegister64(
                        instruction.GetOpRegister(0),
                        SegmentDescriptor.FromBase(
                            this.Memory.GetDescriptorAtSegment((uint)segmentSelector)
                        ).SegmentLimit
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
