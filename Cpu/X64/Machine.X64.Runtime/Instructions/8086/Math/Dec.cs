using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void dec(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Dec_r16:
                {
                    this.ProcessorRegisters.WriteToRegister16(
                        instruction.GetOpRegister(0),
                        (ushort)(this.ProcessorRegisters.EvaluateRegisterValue16(
                            instruction.GetOpRegister(0)
                        ) - (ushort)1)
                    );
                    break;
                }

            case Code.Dec_r32:
                {
                    this.ProcessorRegisters.WriteToRegister32(
                        instruction.GetOpRegister(0),
                        (uint)(this.ProcessorRegisters.EvaluateRegisterValue32(
                            instruction.GetOpRegister(0)
                        ) - 1u)
                    );
                    break;
                }

            case Code.Dec_rm8:
                {
                    RMSet8(in instruction, (byte)(RMEvaluate8(in instruction, 0) - (byte)1), 0);
                    break;
                }

            case Code.Dec_rm16:
                {
                    RMSet16(in instruction, (ushort)(RMEvaluate16(in instruction, 0) - (ushort)1), 0);
                    break;
                }

            case Code.Dec_rm32:
                {
                    RMSet32(in instruction, (uint)(RMEvaluate32(in instruction, 0) - (uint)1), 0);
                    break;
                }

            case Code.Dec_rm64:
                {
                    RMSet64(in instruction, (ulong)(RMEvaluate64(in instruction, 0) - (ulong)1), 0);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
