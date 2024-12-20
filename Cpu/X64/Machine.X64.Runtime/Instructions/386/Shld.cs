using Iced.Intel;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void shld(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Shld_rm16_r16_CL:
                {
                    ushort arg1 = RMEvaluate16(in instruction, 0);
                    ushort arg2 = this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1));
                    byte shift = this.ProcessorRegisters.Cl;

                    ushort value = (ushort)((ushort)(arg1 << shift) | (ushort)(arg2 >> (16 - shift)));
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), value);

                    break;
                }

            case Code.Shld_rm16_r16_imm8:
                {
                    ushort arg1 = RMEvaluate16(in instruction, 0);
                    ushort arg2 = this.ProcessorRegisters.EvaluateRegisterValue16(instruction.GetOpRegister(1));
                    byte shift = (byte)instruction.GetImmediate(2);

                    ushort value = (ushort)((ushort)(arg1 << shift) | (ushort)(arg2 >> (16 - shift)));
                    this.ProcessorRegisters.WriteToRegister16(instruction.GetOpRegister(0), value);

                    break;
                }

            case Code.Shld_rm32_r32_CL:
                {
                    uint arg1 = RMEvaluate32(in instruction, 0);
                    uint arg2 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    byte shift = this.ProcessorRegisters.Cl;

                    uint value = (uint)((uint)(arg1 << shift) | (uint)(arg2 >> (32 - shift)));
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);

                    break;
                }

            case Code.Shld_rm32_r32_imm8:
                {
                    uint arg1 = RMEvaluate32(in instruction, 0);
                    uint arg2 = this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1));
                    byte shift = (byte)instruction.GetImmediate(2);

                    uint value = (uint)((uint)(arg1 << shift) | (uint)(arg2 >> (32 - shift)));
                    this.ProcessorRegisters.WriteToRegister32(instruction.GetOpRegister(0), value);

                    break;
                }

            case Code.Shld_rm64_r64_CL:
                {
                    ulong arg1 = RMEvaluate64(in instruction, 0);
                    ulong arg2 = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));
                    byte shift = this.ProcessorRegisters.Cl;

                    ulong value = (ulong)((ulong)(arg1 << shift) | (ulong)(arg2 >> (64 - shift)));
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);

                    break;
                }

            case Code.Shld_rm64_r64_imm8:
                {
                    ulong arg1 = RMEvaluate64(in instruction, 0);
                    ulong arg2 = this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1));
                    byte shift = (byte)instruction.GetImmediate(2);

                    ulong value = (ulong)((ulong)(arg1 << shift) | (ulong)(arg2 >> (64 - shift)));
                    this.ProcessorRegisters.WriteToRegister64(instruction.GetOpRegister(0), value);

                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
