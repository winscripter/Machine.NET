using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void pinsrw(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Pinsrw_mm_r32m16_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    ushort valueToInsert = 0;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            valueToInsert = BitUtilities.GetLower16Bits(this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            valueToInsert = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                            break;
                    }

                    Vector64<ushort> mmVector = Vector64.Create<ulong>(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, ushort>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mmVector.As<ushort, ulong>()[0]);
                    break;
                }

            case Code.Pinsrw_mm_r64m16_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    ushort valueToInsert = 0;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            valueToInsert = BitUtilities.GetLower16Bits(this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            valueToInsert = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                            break;
                    }

                    Vector64<ushort> mmVector = Vector64.Create<ulong>(this.ProcessorRegisters.EvaluateMM(instruction.GetOpRegister(0))).As<ulong, ushort>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetMM(instruction.GetOpRegister(0), mmVector.As<ushort, ulong>()[0]);
                    break;
                }

            case Code.Pinsrw_xmm_r32m16_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    ushort valueToInsert = 0;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            valueToInsert = BitUtilities.GetLower16Bits(this.ProcessorRegisters.EvaluateRegisterValue32(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            valueToInsert = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                            break;
                    }

                    Vector128<ushort> mmVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), mmVector.As<ushort, float>());
                    break;
                }

            case Code.Pinsrw_xmm_r64m16_imm8:
                {
                    byte imm = (byte)instruction.GetImmediate(1);
                    if (imm > 8)
                    {
                        break;
                    }

                    ushort valueToInsert = 0;
                    OpKind opKind = instruction.GetOpKind(1);

                    switch (opKind)
                    {
                        case OpKind.Register:
                            valueToInsert = BitUtilities.GetLower16Bits(this.ProcessorRegisters.EvaluateRegisterValue64(instruction.GetOpRegister(1)));
                            break;

                        case OpKind.Memory:
                            valueToInsert = this.Memory.ReadUInt16(GetMemOperand16(in instruction));
                            break;
                    }

                    Vector128<ushort> mmVector = this.ProcessorRegisters.EvaluateXmm(instruction.GetOpRegister(0)).As<float, ushort>();
                    mmVector = mmVector.WithElement(imm, valueToInsert);

                    this.ProcessorRegisters.SetXmm(instruction.GetOpRegister(0), mmVector.As<ushort, float>());
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
