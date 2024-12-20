using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pinsrb(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pinsrb_xmm_r32m8_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    byte valueToInsert = 0;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            valueToInsert = BitUtilities.GetLower8Bits(this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            valueToInsert = this.Memory[GetMemOperand16(instruction)];
                            break;
                    }

                    Vector128<byte> mmVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, byte>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), mmVector.As<byte, float>());
                    break;
                }

            case Code.Pinsrb_xmm_r64m8_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    byte valueToInsert = 0;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            valueToInsert = BitUtilities.GetLower8Bits(this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            valueToInsert = this.Memory[GetMemOperand16(instruction)];
                            break;
                    }

                    Vector64<byte> mmVector = Vector64.Create<ulong>(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, byte>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mmVector.As<byte, ulong>()[0]);
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
