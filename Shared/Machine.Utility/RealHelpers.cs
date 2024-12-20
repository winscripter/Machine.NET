namespace Machine.Utility;

public static class RealHelpers
{
    public static (int sign, int exponent, int mantissa, int adjustedExponent) BreakDownFloatingPointInteger(float input)
    {
        int bits = BitConverter.SingleToInt32Bits(input);
        int sign = (bits >> 31) & 1;
        int exponent = (bits >> 23) & 0xFF;
        int mantissa = bits & 0x7FFFFF;
        int adjustedExponent = exponent - 127;

        return (sign, exponent, mantissa, adjustedExponent);
    }

    public static (int sign, int exponent, long mantissa, int adjustedExponent) BreakDownFloatingPointInteger(double input)
    {
        long bits = BitConverter.DoubleToInt64Bits(input);
        int sign = (int)((bits >> 63) & 1);
        int exponent = (int)((bits >> 52) & 0x7FF);
        long mantissa = bits & 0xFFFFFFFFFFFFF;
        int adjustedExponent = exponent - 1023;

        return (sign, exponent, mantissa, adjustedExponent);
    }

    public static bool GetSign(double value)
    {
        long bits = BitConverter.DoubleToInt64Bits(value);
        int sign = (int)((bits >> 63) & 1);
        return sign == 0;
    }

    public static void GetExponentAndMantissa(Half value, out int exponent, out int mantissa)
    {
        ushort rawBits = BitConverter.HalfToUInt16Bits(value);
        exponent = (rawBits >> 10) & 0x1F;
        mantissa = rawBits & 0x3FF;
    }

    public static int GetExponent(Half value)
    {
        ushort rawBits = BitConverter.HalfToUInt16Bits(value);
        return (rawBits >> 10) & 0x1F;
    }

    public static int GetMantissa(Half value)
    {
        ushort rawBits = BitConverter.HalfToUInt16Bits(value);
        return rawBits & 0x3FF;
    }
}
