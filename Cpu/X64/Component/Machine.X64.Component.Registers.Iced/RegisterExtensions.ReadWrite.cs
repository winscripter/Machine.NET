
using Iced.Intel;
using System.Runtime.Intrinsics;

namespace Machine.X64.Component
{
    public static partial class RegisterExtensions
    {
        /// <summary>
        /// Evaluates value of the ZMM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the ZMM register specified by <paramref name="reg" />.
        /// </returns>
        public static Vector512<float> EvaluateZmm(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.ZMM0 => processorRegisters.Zmm0,
                Register.ZMM1 => processorRegisters.Zmm1,
                Register.ZMM2 => processorRegisters.Zmm2,
                Register.ZMM3 => processorRegisters.Zmm3,
                Register.ZMM4 => processorRegisters.Zmm4,
                Register.ZMM5 => processorRegisters.Zmm5,
                Register.ZMM6 => processorRegisters.Zmm6,
                Register.ZMM7 => processorRegisters.Zmm7,
                Register.ZMM8 => processorRegisters.Zmm8,
                Register.ZMM9 => processorRegisters.Zmm9,
                Register.ZMM10 => processorRegisters.Zmm10,
                Register.ZMM11 => processorRegisters.Zmm11,
                Register.ZMM12 => processorRegisters.Zmm12,
                Register.ZMM13 => processorRegisters.Zmm13,
                Register.ZMM14 => processorRegisters.Zmm14,
                Register.ZMM15 => processorRegisters.Zmm15,
                Register.ZMM16 => processorRegisters.Zmm16,
                Register.ZMM17 => processorRegisters.Zmm17,
                Register.ZMM18 => processorRegisters.Zmm18,
                Register.ZMM19 => processorRegisters.Zmm19,
                Register.ZMM20 => processorRegisters.Zmm20,
                Register.ZMM21 => processorRegisters.Zmm21,
                Register.ZMM22 => processorRegisters.Zmm22,
                Register.ZMM23 => processorRegisters.Zmm23,
                Register.ZMM24 => processorRegisters.Zmm24,
                Register.ZMM25 => processorRegisters.Zmm25,
                Register.ZMM26 => processorRegisters.Zmm26,
                Register.ZMM27 => processorRegisters.Zmm27,
                Register.ZMM28 => processorRegisters.Zmm28,
                Register.ZMM29 => processorRegisters.Zmm29,
                Register.ZMM30 => processorRegisters.Zmm30,
                Register.ZMM31 => processorRegisters.Zmm31,
                _ => throw new ArgumentException("Input register is not a valid ZMM register", nameof(reg))
            };
        }

        /// <summary>
        /// Evaluates value of the YMM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the YMM register specified by <paramref name="reg" />.
        /// </returns>
        public static Vector256<float> EvaluateYmm(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.YMM0 => processorRegisters.Ymm0,
                Register.YMM1 => processorRegisters.Ymm1,
                Register.YMM2 => processorRegisters.Ymm2,
                Register.YMM3 => processorRegisters.Ymm3,
                Register.YMM4 => processorRegisters.Ymm4,
                Register.YMM5 => processorRegisters.Ymm5,
                Register.YMM6 => processorRegisters.Ymm6,
                Register.YMM7 => processorRegisters.Ymm7,
                Register.YMM8 => processorRegisters.Ymm8,
                Register.YMM9 => processorRegisters.Ymm9,
                Register.YMM10 => processorRegisters.Ymm10,
                Register.YMM11 => processorRegisters.Ymm11,
                Register.YMM12 => processorRegisters.Ymm12,
                Register.YMM13 => processorRegisters.Ymm13,
                Register.YMM14 => processorRegisters.Ymm14,
                Register.YMM15 => processorRegisters.Ymm15,
                Register.YMM16 => processorRegisters.Ymm16,
                Register.YMM17 => processorRegisters.Ymm17,
                Register.YMM18 => processorRegisters.Ymm18,
                Register.YMM19 => processorRegisters.Ymm19,
                Register.YMM20 => processorRegisters.Ymm20,
                Register.YMM21 => processorRegisters.Ymm21,
                Register.YMM22 => processorRegisters.Ymm22,
                Register.YMM23 => processorRegisters.Ymm23,
                Register.YMM24 => processorRegisters.Ymm24,
                Register.YMM25 => processorRegisters.Ymm25,
                Register.YMM26 => processorRegisters.Ymm26,
                Register.YMM27 => processorRegisters.Ymm27,
                Register.YMM28 => processorRegisters.Ymm28,
                Register.YMM29 => processorRegisters.Ymm29,
                Register.YMM30 => processorRegisters.Ymm30,
                Register.YMM31 => processorRegisters.Ymm31,
                _ => throw new ArgumentException("Input register is not a valid YMM register", nameof(reg))
            };
        }

        /// <summary>
        /// Evaluates value of the XMM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the XMM register specified by <paramref name="reg" />.
        /// </returns>
        public static Vector128<float> EvaluateXmm(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.XMM0 => processorRegisters.Xmm0,
                Register.XMM1 => processorRegisters.Xmm1,
                Register.XMM2 => processorRegisters.Xmm2,
                Register.XMM3 => processorRegisters.Xmm3,
                Register.XMM4 => processorRegisters.Xmm4,
                Register.XMM5 => processorRegisters.Xmm5,
                Register.XMM6 => processorRegisters.Xmm6,
                Register.XMM7 => processorRegisters.Xmm7,
                Register.XMM8 => processorRegisters.Xmm8,
                Register.XMM9 => processorRegisters.Xmm9,
                Register.XMM10 => processorRegisters.Xmm10,
                Register.XMM11 => processorRegisters.Xmm11,
                Register.XMM12 => processorRegisters.Xmm12,
                Register.XMM13 => processorRegisters.Xmm13,
                Register.XMM14 => processorRegisters.Xmm14,
                Register.XMM15 => processorRegisters.Xmm15,
                Register.XMM16 => processorRegisters.Xmm16,
                Register.XMM17 => processorRegisters.Xmm17,
                Register.XMM18 => processorRegisters.Xmm18,
                Register.XMM19 => processorRegisters.Xmm19,
                Register.XMM20 => processorRegisters.Xmm20,
                Register.XMM21 => processorRegisters.Xmm21,
                Register.XMM22 => processorRegisters.Xmm22,
                Register.XMM23 => processorRegisters.Xmm23,
                Register.XMM24 => processorRegisters.Xmm24,
                Register.XMM25 => processorRegisters.Xmm25,
                Register.XMM26 => processorRegisters.Xmm26,
                Register.XMM27 => processorRegisters.Xmm27,
                Register.XMM28 => processorRegisters.Xmm28,
                Register.XMM29 => processorRegisters.Xmm29,
                Register.XMM30 => processorRegisters.Xmm30,
                Register.XMM31 => processorRegisters.Xmm31,
                _ => throw new ArgumentException("Input register is not a valid XMM register", nameof(reg))
            };
        }

        /// <summary>
        /// Alters the value of the ZMM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetZmm(this ProcessorRegisters processorRegisters, Register reg, Vector512<float> value)
        {
            switch (reg)
            {
                case Register.ZMM0:
                    processorRegisters.Zmm0 = value;
                    break;

                case Register.ZMM1:
                    processorRegisters.Zmm1 = value;
                    break;

                case Register.ZMM2:
                    processorRegisters.Zmm2 = value;
                    break;

                case Register.ZMM3:
                    processorRegisters.Zmm3 = value;
                    break;

                case Register.ZMM4:
                    processorRegisters.Zmm4 = value;
                    break;

                case Register.ZMM5:
                    processorRegisters.Zmm5 = value;
                    break;

                case Register.ZMM6:
                    processorRegisters.Zmm6 = value;
                    break;

                case Register.ZMM7:
                    processorRegisters.Zmm7 = value;
                    break;

                case Register.ZMM8:
                    processorRegisters.Zmm8 = value;
                    break;

                case Register.ZMM9:
                    processorRegisters.Zmm9 = value;
                    break;

                case Register.ZMM10:
                    processorRegisters.Zmm10 = value;
                    break;

                case Register.ZMM11:
                    processorRegisters.Zmm11 = value;
                    break;

                case Register.ZMM12:
                    processorRegisters.Zmm12 = value;
                    break;

                case Register.ZMM13:
                    processorRegisters.Zmm13 = value;
                    break;

                case Register.ZMM14:
                    processorRegisters.Zmm14 = value;
                    break;

                case Register.ZMM15:
                    processorRegisters.Zmm15 = value;
                    break;

                case Register.ZMM16:
                    processorRegisters.Zmm16 = value;
                    break;

                case Register.ZMM17:
                    processorRegisters.Zmm17 = value;
                    break;

                case Register.ZMM18:
                    processorRegisters.Zmm18 = value;
                    break;

                case Register.ZMM19:
                    processorRegisters.Zmm19 = value;
                    break;

                case Register.ZMM20:
                    processorRegisters.Zmm20 = value;
                    break;

                case Register.ZMM21:
                    processorRegisters.Zmm21 = value;
                    break;

                case Register.ZMM22:
                    processorRegisters.Zmm22 = value;
                    break;

                case Register.ZMM23:
                    processorRegisters.Zmm23 = value;
                    break;

                case Register.ZMM24:
                    processorRegisters.Zmm24 = value;
                    break;

                case Register.ZMM25:
                    processorRegisters.Zmm25 = value;
                    break;

                case Register.ZMM26:
                    processorRegisters.Zmm26 = value;
                    break;

                case Register.ZMM27:
                    processorRegisters.Zmm27 = value;
                    break;

                case Register.ZMM28:
                    processorRegisters.Zmm28 = value;
                    break;

                case Register.ZMM29:
                    processorRegisters.Zmm29 = value;
                    break;

                case Register.ZMM30:
                    processorRegisters.Zmm30 = value;
                    break;

                case Register.ZMM31:
                    processorRegisters.Zmm31 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid ZMM register", nameof(reg));
            }
        }

        /// <summary>
        /// Alters the value of the YMM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetYmm(this ProcessorRegisters processorRegisters, Register reg, Vector256<float> value)
        {
            switch (reg)
            {
                case Register.YMM0:
                    processorRegisters.Ymm0 = value;
                    break;

                case Register.YMM1:
                    processorRegisters.Ymm1 = value;
                    break;

                case Register.YMM2:
                    processorRegisters.Ymm2 = value;
                    break;

                case Register.YMM3:
                    processorRegisters.Ymm3 = value;
                    break;

                case Register.YMM4:
                    processorRegisters.Ymm4 = value;
                    break;

                case Register.YMM5:
                    processorRegisters.Ymm5 = value;
                    break;

                case Register.YMM6:
                    processorRegisters.Ymm6 = value;
                    break;

                case Register.YMM7:
                    processorRegisters.Ymm7 = value;
                    break;

                case Register.YMM8:
                    processorRegisters.Ymm8 = value;
                    break;

                case Register.YMM9:
                    processorRegisters.Ymm9 = value;
                    break;

                case Register.YMM10:
                    processorRegisters.Ymm10 = value;
                    break;

                case Register.YMM11:
                    processorRegisters.Ymm11 = value;
                    break;

                case Register.YMM12:
                    processorRegisters.Ymm12 = value;
                    break;

                case Register.YMM13:
                    processorRegisters.Ymm13 = value;
                    break;

                case Register.YMM14:
                    processorRegisters.Ymm14 = value;
                    break;

                case Register.YMM15:
                    processorRegisters.Ymm15 = value;
                    break;

                case Register.YMM16:
                    processorRegisters.Ymm16 = value;
                    break;

                case Register.YMM17:
                    processorRegisters.Ymm17 = value;
                    break;

                case Register.YMM18:
                    processorRegisters.Ymm18 = value;
                    break;

                case Register.YMM19:
                    processorRegisters.Ymm19 = value;
                    break;

                case Register.YMM20:
                    processorRegisters.Ymm20 = value;
                    break;

                case Register.YMM21:
                    processorRegisters.Ymm21 = value;
                    break;

                case Register.YMM22:
                    processorRegisters.Ymm22 = value;
                    break;

                case Register.YMM23:
                    processorRegisters.Ymm23 = value;
                    break;

                case Register.YMM24:
                    processorRegisters.Ymm24 = value;
                    break;

                case Register.YMM25:
                    processorRegisters.Ymm25 = value;
                    break;

                case Register.YMM26:
                    processorRegisters.Ymm26 = value;
                    break;

                case Register.YMM27:
                    processorRegisters.Ymm27 = value;
                    break;

                case Register.YMM28:
                    processorRegisters.Ymm28 = value;
                    break;

                case Register.YMM29:
                    processorRegisters.Ymm29 = value;
                    break;

                case Register.YMM30:
                    processorRegisters.Ymm30 = value;
                    break;

                case Register.YMM31:
                    processorRegisters.Ymm31 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid YMM register", nameof(reg));
            }
        }

        /// <summary>
        /// Alters the value of the XMM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetXmm(this ProcessorRegisters processorRegisters, Register reg, Vector128<float> value)
        {
            switch (reg)
            {
                case Register.XMM0:
                    processorRegisters.Xmm0 = value;
                    break;

                case Register.XMM1:
                    processorRegisters.Xmm1 = value;
                    break;

                case Register.XMM2:
                    processorRegisters.Xmm2 = value;
                    break;

                case Register.XMM3:
                    processorRegisters.Xmm3 = value;
                    break;

                case Register.XMM4:
                    processorRegisters.Xmm4 = value;
                    break;

                case Register.XMM5:
                    processorRegisters.Xmm5 = value;
                    break;

                case Register.XMM6:
                    processorRegisters.Xmm6 = value;
                    break;

                case Register.XMM7:
                    processorRegisters.Xmm7 = value;
                    break;

                case Register.XMM8:
                    processorRegisters.Xmm8 = value;
                    break;

                case Register.XMM9:
                    processorRegisters.Xmm9 = value;
                    break;

                case Register.XMM10:
                    processorRegisters.Xmm10 = value;
                    break;

                case Register.XMM11:
                    processorRegisters.Xmm11 = value;
                    break;

                case Register.XMM12:
                    processorRegisters.Xmm12 = value;
                    break;

                case Register.XMM13:
                    processorRegisters.Xmm13 = value;
                    break;

                case Register.XMM14:
                    processorRegisters.Xmm14 = value;
                    break;

                case Register.XMM15:
                    processorRegisters.Xmm15 = value;
                    break;

                case Register.XMM16:
                    processorRegisters.Xmm16 = value;
                    break;

                case Register.XMM17:
                    processorRegisters.Xmm17 = value;
                    break;

                case Register.XMM18:
                    processorRegisters.Xmm18 = value;
                    break;

                case Register.XMM19:
                    processorRegisters.Xmm19 = value;
                    break;

                case Register.XMM20:
                    processorRegisters.Xmm20 = value;
                    break;

                case Register.XMM21:
                    processorRegisters.Xmm21 = value;
                    break;

                case Register.XMM22:
                    processorRegisters.Xmm22 = value;
                    break;

                case Register.XMM23:
                    processorRegisters.Xmm23 = value;
                    break;

                case Register.XMM24:
                    processorRegisters.Xmm24 = value;
                    break;

                case Register.XMM25:
                    processorRegisters.Xmm25 = value;
                    break;

                case Register.XMM26:
                    processorRegisters.Xmm26 = value;
                    break;

                case Register.XMM27:
                    processorRegisters.Xmm27 = value;
                    break;

                case Register.XMM28:
                    processorRegisters.Xmm28 = value;
                    break;

                case Register.XMM29:
                    processorRegisters.Xmm29 = value;
                    break;

                case Register.XMM30:
                    processorRegisters.Xmm30 = value;
                    break;

                case Register.XMM31:
                    processorRegisters.Xmm31 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid XMM register", nameof(reg));
            }
        }

        /// <summary>
        /// Evaluates value of the CR register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the CR register specified by <paramref name="reg" />.
        /// </returns>
        public static ulong EvaluateCR(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.CR0 => processorRegisters.CR0,
                Register.CR1 => processorRegisters.CR1,
                Register.CR2 => processorRegisters.CR2,
                Register.CR3 => processorRegisters.CR3,
                Register.CR4 => processorRegisters.CR4,
                Register.CR5 => processorRegisters.CR5,
                Register.CR6 => processorRegisters.CR6,
                Register.CR7 => processorRegisters.CR7,
                _ => throw new ArgumentException("Input register is not a valid CR register", nameof(reg))
            };
        }

        /// <summary>
        /// Evaluates value of the MM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the MM register specified by <paramref name="reg" />.
        /// </returns>
        public static ulong EvaluateMM(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.MM0 => processorRegisters.MM0,
                Register.MM1 => processorRegisters.MM1,
                Register.MM2 => processorRegisters.MM2,
                Register.MM3 => processorRegisters.MM3,
                Register.MM4 => processorRegisters.MM4,
                Register.MM5 => processorRegisters.MM5,
                Register.MM6 => processorRegisters.MM6,
                Register.MM7 => processorRegisters.MM7,
                _ => throw new ArgumentException("Input register is not a valid MM register", nameof(reg))
            };
        }

        /// <summary>
        /// Evaluates value of the DR register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the DR register specified by <paramref name="reg" />.
        /// </returns>
        public static ulong EvaluateDR(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.DR0 => processorRegisters.DR0,
                Register.DR1 => processorRegisters.DR1,
                Register.DR2 => processorRegisters.DR2,
                Register.DR3 => processorRegisters.DR3,
                Register.DR4 => processorRegisters.DR4,
                Register.DR5 => processorRegisters.DR5,
                Register.DR6 => processorRegisters.DR6,
                Register.DR7 => processorRegisters.DR7,
                _ => throw new ArgumentException("Input register is not a valid DR register", nameof(reg))
            };
        }

        /// <summary>
        /// Evaluates value of the TR register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the TR register specified by <paramref name="reg" />.
        /// </returns>
        public static ulong EvaluateTR(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.TR0 => processorRegisters.TR0,
                Register.TR1 => processorRegisters.TR1,
                Register.TR2 => processorRegisters.TR2,
                Register.TR3 => processorRegisters.TR3,
                Register.TR4 => processorRegisters.TR4,
                Register.TR5 => processorRegisters.TR5,
                Register.TR6 => processorRegisters.TR6,
                Register.TR7 => processorRegisters.TR7,
                _ => throw new ArgumentException("Input register is not a valid TR register", nameof(reg))
            };
        }

        /// <summary>
        /// Alters the value of the CR register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetCR(this ProcessorRegisters processorRegisters, Register reg, ulong value)
        {
            switch (reg)
            {
                case Register.CR0:
                    processorRegisters.CR0 = value;
                    break;

                case Register.CR1:
                    processorRegisters.CR1 = value;
                    break;

                case Register.CR2:
                    processorRegisters.CR2 = value;
                    break;

                case Register.CR3:
                    processorRegisters.CR3 = value;
                    break;

                case Register.CR4:
                    processorRegisters.CR4 = value;
                    break;

                case Register.CR5:
                    processorRegisters.CR5 = value;
                    break;

                case Register.CR6:
                    processorRegisters.CR6 = value;
                    break;

                case Register.CR7:
                    processorRegisters.CR7 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid CR register", nameof(reg));
            }
        }

        /// <summary>
        /// Alters the value of the MM register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetMM(this ProcessorRegisters processorRegisters, Register reg, ulong value)
        {
            switch (reg)
            {
                case Register.MM0:
                    processorRegisters.MM0 = value;
                    break;

                case Register.MM1:
                    processorRegisters.MM1 = value;
                    break;

                case Register.MM2:
                    processorRegisters.MM2 = value;
                    break;

                case Register.MM3:
                    processorRegisters.MM3 = value;
                    break;

                case Register.MM4:
                    processorRegisters.MM4 = value;
                    break;

                case Register.MM5:
                    processorRegisters.MM5 = value;
                    break;

                case Register.MM6:
                    processorRegisters.MM6 = value;
                    break;

                case Register.MM7:
                    processorRegisters.MM7 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid MM register", nameof(reg));
            }
        }

        /// <summary>
        /// Alters the value of the DR register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetDR(this ProcessorRegisters processorRegisters, Register reg, ulong value)
        {
            switch (reg)
            {
                case Register.DR0:
                    processorRegisters.DR0 = value;
                    break;

                case Register.DR1:
                    processorRegisters.DR1 = value;
                    break;

                case Register.DR2:
                    processorRegisters.DR2 = value;
                    break;

                case Register.DR3:
                    processorRegisters.DR3 = value;
                    break;

                case Register.DR4:
                    processorRegisters.DR4 = value;
                    break;

                case Register.DR5:
                    processorRegisters.DR5 = value;
                    break;

                case Register.DR6:
                    processorRegisters.DR6 = value;
                    break;

                case Register.DR7:
                    processorRegisters.DR7 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid DR register", nameof(reg));
            }
        }

        /// <summary>
        /// Alters the value of the TR register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetTR(this ProcessorRegisters processorRegisters, Register reg, ulong value)
        {
            switch (reg)
            {
                case Register.TR0:
                    processorRegisters.TR0 = value;
                    break;

                case Register.TR1:
                    processorRegisters.TR1 = value;
                    break;

                case Register.TR2:
                    processorRegisters.TR2 = value;
                    break;

                case Register.TR3:
                    processorRegisters.TR3 = value;
                    break;

                case Register.TR4:
                    processorRegisters.TR4 = value;
                    break;

                case Register.TR5:
                    processorRegisters.TR5 = value;
                    break;

                case Register.TR6:
                    processorRegisters.TR6 = value;
                    break;

                case Register.TR7:
                    processorRegisters.TR7 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid TR register", nameof(reg));
            }
        }

        /// <summary>
        /// Alters the value of the K register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <param name="value">Value</param>
        public static void SetK(this ProcessorRegisters processorRegisters, Register reg, ulong value)
        {
            switch (reg)
            {
                case Register.K0:
                    processorRegisters.K0 = value;
                    break;

                case Register.K1:
                    processorRegisters.K1 = value;
                    break;

                case Register.K2:
                    processorRegisters.K2 = value;
                    break;

                case Register.K3:
                    processorRegisters.K3 = value;
                    break;

                case Register.K4:
                    processorRegisters.K4 = value;
                    break;

                case Register.K5:
                    processorRegisters.K5 = value;
                    break;

                case Register.K6:
                    processorRegisters.K6 = value;
                    break;

                case Register.K7:
                    processorRegisters.K7 = value;
                    break;

                default:
                    throw new ArgumentException("Input register is not a valid K register", nameof(reg));
            }
        }

        /// <summary>
        /// Evaluates value of the K register.
        /// </summary>
        /// <param name="processorRegisters">Processor registers</param>
        /// <param name="reg">Register</param>
        /// <returns>
        /// Value of the K register specified by <paramref name="reg" />.
        /// </returns>
        public static ulong EvaluateK(this ProcessorRegisters processorRegisters, Register reg)
        {
            return reg switch
            {
                Register.K0 => processorRegisters.K0,
                Register.K1 => processorRegisters.K1,
                Register.K2 => processorRegisters.K2,
                Register.K3 => processorRegisters.K3,
                Register.K4 => processorRegisters.K4,
                Register.K5 => processorRegisters.K5,
                Register.K6 => processorRegisters.K6,
                Register.K7 => processorRegisters.K7,
                _ => throw new ArgumentException("Input register is not a valid K register", nameof(reg))
            };
        }
    }
}
