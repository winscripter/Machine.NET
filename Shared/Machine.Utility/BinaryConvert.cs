using System.Buffers.Binary;

namespace Machine.Utility;

internal static class BinaryConvert
{
    public static long DoubleToInt64Bits(double value)
    {
        var buffer = new Span<byte>(new byte[8]); // 8 bytes because double and long are 8 bytes in size
        BinaryPrimitives.WriteDoubleBigEndian(buffer, value);

        return BinaryPrimitives.ReadInt64BigEndian(buffer);
    }

    public static double Int64BitsToDouble(long value)
    {
        var buffer = new Span<byte>(new byte[8]); // 8 bytes because long and double are 8 bytes in size
        BinaryPrimitives.WriteInt64BigEndian(buffer, value);

        return BinaryPrimitives.ReadDoubleBigEndian(buffer);
    }

    public static float Int32ToSingle(int value)
    {
        var buffer = new Span<byte>(new byte[4]); // 4 bytes because int and float are 4 bytes in size
        BinaryPrimitives.WriteInt64BigEndian(buffer, value);

        return BinaryPrimitives.ReadSingleBigEndian(buffer);
    }

    public static int SingleToInt32(float value)
    {
        var buffer = new Span<byte>(new byte[4]); // 4 bytes because float and int are 4 bytes in size
        BinaryPrimitives.WriteSingleBigEndian(buffer, value);

        return BinaryPrimitives.ReadInt32BigEndian(buffer);
    }
}
