using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    private void movbe(in Instruction instruction)
    {
        switch (instruction.Code)
        {
            case Code.Movbe_m16_r16:
                {
                    this.Memory.WriteUInt16(
                        GetMemOperand(in instruction), 
                        BitUtilities.ByteOrderSwap(
                            this.ProcessorRegisters.EvaluateRegisterValue16(
                                instruction.GetOpRegister(1)
                            )
                        )
                    );
                    break;
                }

            case Code.Movbe_r16_m16:
                {
                    this.Memory.WriteUInt16(
                        GetMemOperand(in instruction),
                        BitUtilities.ByteOrderSwap(
                            this.Memory.ReadUInt16(GetMemOperand(in instruction))
                        )
                    );
                    break;
                }

            case Code.Movbe_m32_r32:
                {
                    this.Memory.WriteUInt32(
                        GetMemOperand(in instruction),
                        BitUtilities.ByteOrderSwap(
                            this.ProcessorRegisters.EvaluateRegisterValue32(
                                instruction.GetOpRegister(1)
                            )
                        )
                    );
                    break;
                }

            case Code.Movbe_r32_m32:
                {
                    this.Memory.WriteUInt32(
                        GetMemOperand(in instruction),
                        BitUtilities.ByteOrderSwap(
                            this.Memory.ReadUInt32(GetMemOperand(in instruction))
                        )
                    );
                    break;
                }

            case Code.Movbe_m64_r64:
                {
                    this.Memory.WriteUInt64(
                        GetMemOperand(in instruction),
                        BitUtilities.ByteOrderSwap(
                            this.ProcessorRegisters.EvaluateRegisterValue64(
                                instruction.GetOpRegister(1)
                            )
                        )
                    );
                    break;
                }

            case Code.Movbe_r64_m64:
                {
                    this.Memory.WriteUInt64(
                        GetMemOperand(in instruction),
                        BitUtilities.ByteOrderSwap(
                            this.Memory.ReadUInt64(GetMemOperand(in instruction))
                        )
                    );
                    break;
                }

            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }
    }
}
