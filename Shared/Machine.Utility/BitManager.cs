using System.Buffers.Binary;

namespace Machine.Utility;

public static class BitManager
{
    public static Span<bool> GetBitsBigEndian(byte b)
    {
        var span = new Span<bool>(new bool[BitCounts.Byte]);
        for (int i = 0; i < BitCounts.Byte; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.Byte - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(byte b)
    {
        var span = new Span<bool>(new bool[BitCounts.Byte]);
        for (int i = 0; i < BitCounts.Byte; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.Byte - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(ushort b)
    {
        var span = new Span<bool>(new bool[BitCounts.UInt16]);
        for (int i = 0; i < BitCounts.UInt16; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.UInt16 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(ushort b)
    {
        var span = new Span<bool>(new bool[BitCounts.UInt16]);
        for (int i = 0; i < BitCounts.UInt16; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.UInt16 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(uint b)
    {
        var span = new Span<bool>(new bool[BitCounts.UInt32]);
        for (int i = 0; i < BitCounts.UInt32; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.UInt32 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(uint b)
    {
        var span = new Span<bool>(new bool[BitCounts.UInt32]);
        for (int i = 0; i < BitCounts.UInt32; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.UInt32 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(ulong b)
    {
        var span = new Span<bool>(new bool[BitCounts.UInt64]);
        for (int i = 0; i < BitCounts.UInt64; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.UInt64 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(ulong b)
    {
        var span = new Span<bool>(new bool[BitCounts.UInt64]);
        for (int i = 0; i < BitCounts.UInt64; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.UInt64 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(short b)
    {
        var span = new Span<bool>(new bool[BitCounts.Int16]);
        for (int i = 0; i < BitCounts.Int16; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.Int16 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(short b)
    {
        var span = new Span<bool>(new bool[BitCounts.Int16]);
        for (int i = 0; i < BitCounts.Int16; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.Int16 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(int b)
    {
        var span = new Span<bool>(new bool[BitCounts.Int32]);
        for (int i = 0; i < BitCounts.Int32; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.Int32 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(int b)
    {
        var span = new Span<bool>(new bool[BitCounts.Int32]);
        for (int i = 0; i < BitCounts.Int32; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.Int32 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(long b)
    {
        var span = new Span<bool>(new bool[BitCounts.Int64]);
        for (int i = 0; i < BitCounts.Int64; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, false, BitCounts.Int64 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsLittleEndian(long b)
    {
        var span = new Span<bool>(new bool[BitCounts.Int64]);
        for (int i = 0; i < BitCounts.Int64; i++)
        {
            span[i] = BitUtilities.IsBitSet(b, i, true, BitCounts.Int64 - 1);
        }
        return span;
    }

    public static Span<bool> GetBitsBigEndian(float b)
    {
        var binary = new Span<byte>(new byte[BitCounts.Single / 8]);
        BinaryPrimitives.WriteSingleBigEndian(binary, b);

        var span = new Span<bool>(new bool[BitCounts.Single]);
        for (int i = 0; i < BitCounts.Single / 8; i++)
        {
            byte current = binary[i];
            Span<bool> currentAsBinary = GetBitsBigEndian(current);
            for (int j = 0; j < BitCounts.Byte; j++)
            {
                span[i * BitCounts.Byte + j] = currentAsBinary[j];
            }
        }

        return span;
    }

    public static Span<bool> GetBitsLittleEndian(float b)
    {
        var binary = new Span<byte>(new byte[BitCounts.Single / 8]);
        BinaryPrimitives.WriteSingleLittleEndian(binary, b);

        var span = new Span<bool>(new bool[BitCounts.Single]);
        for (int i = 0; i < BitCounts.Single / 8; i++)
        {
            byte current = binary[i];
            Span<bool> currentAsBinary = GetBitsBigEndian(current);
            for (int j = 0; j < BitCounts.Byte; j++)
            {
                span[i * BitCounts.Byte + j] = currentAsBinary[j];
            }
        }

        return span;
    }

    public static Span<bool> GetBitsBigEndian(double b)
    {
        var binary = new Span<byte>(new byte[BitCounts.Double / 8]);
        BinaryPrimitives.WriteDoubleBigEndian(binary, b);

        var span = new Span<bool>(new bool[BitCounts.Double]);
        for (int i = 0; i < BitCounts.Double / 8; i++)
        {
            byte current = binary[i];
            Span<bool> currentAsBinary = GetBitsBigEndian(current);
            for (int j = 0; j < BitCounts.Byte; j++)
            {
                span[i * BitCounts.Byte + j] = currentAsBinary[j];
            }
        }

        return span;
    }

    public static Span<bool> GetBitsLittleEndian(double b)
    {
        var binary = new Span<byte>(new byte[BitCounts.Double / 8]);
        BinaryPrimitives.WriteDoubleLittleEndian(binary, b);

        var span = new Span<bool>(new bool[BitCounts.Double]);
        for (int i = 0; i < BitCounts.Double / 8; i++)
        {
            byte current = binary[i];
            Span<bool> currentAsBinary = GetBitsBigEndian(current);
            for (int j = 0; j < BitCounts.Byte; j++)
            {
                span[i * BitCounts.Byte + j] = currentAsBinary[j];
            }
        }

        return span;
    }

    public static byte ReadByteLittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Byte)
            throw new ArgumentException($"Length of the span must be {BitCounts.Byte}", nameof(span));

        byte result = 0;
        for (int i = 0; i < BitCounts.Byte; i++)
        {
            if (span[i])
            {
                result |= (byte)(1 << i);
            }
        }

        return result;
    }

    public static byte ReadByteBigEndian(ReadOnlySpan<bool> span)
    {
        if (span.Length != BitCounts.Byte)
            throw new ArgumentException($"Length of the span must be {BitCounts.Byte}", nameof(span));

        byte result = 0;
        for (int i = 0; i < 8; i++)
        {
            if (span[i])
            {
                result |= (byte)(1 << 7 - i);
            }
        }

        return result;
    }

    public static ushort ReadUInt16LittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.UInt16)
            throw new ArgumentException($"Length of the span must be {BitCounts.UInt16}", nameof(span));

        ushort result = 0;
        for (int i = 0; i < BitCounts.UInt16; i++)
        {
            if (span[i])
            {
                result |= (ushort)(1 << i);
            }
        }

        return result;
    }

    public static ushort ReadUInt16BigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.UInt16)
            throw new ArgumentException($"Length of the span must be {BitCounts.UInt16}", nameof(span));

        ushort result = 0;
        for (int i = BitCounts.UInt16; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= (ushort)(1 << i - 1);
            }
        }

        return result;
    }

    public static uint ReadUInt32LittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.UInt32)
            throw new ArgumentException($"Length of the span must be {BitCounts.UInt32}", nameof(span));

        uint result = 0;
        for (int i = 0; i < BitCounts.UInt32; i++)
        {
            if (span[i])
            {
                result |= (uint)(1 << i);
            }
        }

        return result;
    }

    public static uint ReadUInt32BigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.UInt32)
            throw new ArgumentException($"Length of the span must be {BitCounts.UInt32}", nameof(span));

        uint result = 0;
        for (int i = BitCounts.UInt32; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= (uint)(1 << i - 1);
            }
        }

        return result;
    }

    public static ulong ReadUInt64LittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.UInt64)
            throw new ArgumentException($"Length of the span must be {BitCounts.UInt64}", nameof(span));

        ulong result = 0;
        for (int i = 0; i < BitCounts.UInt64; i++)
        {
            if (span[i])
            {
                result |= 1UL << i;
            }
        }

        return result;
    }

    public static ulong ReadUInt64BigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.UInt64)
            throw new ArgumentException($"Length of the span must be {BitCounts.UInt64}", nameof(span));

        ulong result = 0;
        for (int i = BitCounts.UInt64; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= 1uL << i - 1;
            }
        }

        return result;
    }

    public static sbyte ReadSByteLittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.SByte)
            throw new ArgumentException($"Length of the span must be {BitCounts.SByte}", nameof(span));

        sbyte result = 0;
        for (int i = 0; i < BitCounts.SByte; i++)
        {
            if (span[i])
            {
                result |= (sbyte)(1 << i);
            }
        }

        return result;
    }

    public static sbyte ReadSByteBigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.SByte)
            throw new ArgumentException($"Length of the span must be {BitCounts.SByte}", nameof(span));

        sbyte result = 0;
        for (int i = BitCounts.SByte; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= (sbyte)(1 << i - 1);
            }
        }

        return result;
    }

    public static short ReadInt16LittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Int16)
            throw new ArgumentException($"Length of the span must be {BitCounts.Int16}", nameof(span));

        short result = 0;
        for (int i = 0; i < BitCounts.Int16; i++)
        {
            if (span[i])
            {
                result |= (short)(1 << i);
            }
        }

        return result;
    }

    public static short ReadInt16BigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Int16)
            throw new ArgumentException($"Length of the span must be {BitCounts.Int16}", nameof(span));

        short result = 0;
        for (int i = BitCounts.Int16; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= (short)(1 << i - 1);
            }
        }

        return result;
    }

    public static int ReadInt32LittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Int32)
            throw new ArgumentException($"Length of the span must be {BitCounts.Int32}", nameof(span));

        int result = 0;
        for (int i = 0; i < BitCounts.Int32; i++)
        {
            if (span[i])
            {
                result |= 1 << i;
            }
        }

        return result;
    }

    public static int ReadInt32BigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Int32)
            throw new ArgumentException($"Length of the span must be {BitCounts.Int32}", nameof(span));

        int result = 0;
        for (int i = BitCounts.Int32; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= 1 << i - 1;
            }
        }

        return result;
    }

    public static long ReadInt64LittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Int64)
            throw new ArgumentException($"Length of the span must be {BitCounts.Int64}", nameof(span));

        long result = 0;
        for (int i = 0; i < BitCounts.Int64; i++)
        {
            if (span[i])
            {
                result |= (long)(1 << i);
            }
        }

        return result;
    }

    public static long ReadInt64BigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Int64)
            throw new ArgumentException($"Length of the span must be {BitCounts.Int64}", nameof(span));

        long result = 0;
        for (int i = BitCounts.Int64; i > 0; i--)
        {
            if (span[i - 1])
            {
                result |= (long)(1 << i - 1);
            }
        }

        return result;
    }

    public static float ReadSingleLittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Single)
            throw new ArgumentException($"Length of the span must be {BitCounts.Single}", nameof(span));

        var newSpan = new Span<byte>(new byte[BitCounts.Single / 8]);
        for (int i = 0; i < BitCounts.Single / 8; i++)
        {
            newSpan[i] = ReadByteBigEndian(span.Slice(i * 8, BitCounts.Byte));
        }

        return BinaryPrimitives.ReadSingleLittleEndian(newSpan);
    }

    public static float ReadSingleBigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Single)
            throw new ArgumentException($"Length of the span must be {BitCounts.Single}", nameof(span));

        var newSpan = new Span<byte>(new byte[BitCounts.Single / 8]);
        for (int i = 0; i < BitCounts.Single / 8; i++)
        {
            newSpan[i] = ReadByteBigEndian(span.Slice(i * BitCounts.Byte, BitCounts.Byte));
        }

        return BinaryPrimitives.ReadSingleBigEndian(newSpan);
    }

    public static double ReadDoubleLittleEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Double)
            throw new ArgumentException($"Length of the span must be {BitCounts.Double}", nameof(span));

        var newSpan = new Span<byte>(new byte[BitCounts.Double / 8]);
        for (int i = 0; i < BitCounts.Double / 8; i++)
        {
            newSpan[i] = ReadByteBigEndian(span.Slice(i * 8, BitCounts.Byte));
        }

        return BinaryPrimitives.ReadDoubleLittleEndian(newSpan);
    }

    public static double ReadDoubleBigEndian(Span<bool> span)
    {
        if (span.Length != BitCounts.Double)
            throw new ArgumentException($"Length of the span must be {BitCounts.Double}", nameof(span));

        var newSpan = new Span<byte>(new byte[BitCounts.Double / 8]);
        for (int i = 0; i < BitCounts.Double / 8; i++)
        {
            newSpan[i] = ReadByteBigEndian(span.Slice(i * BitCounts.Byte, BitCounts.Byte));
        }

        return BinaryPrimitives.ReadDoubleBigEndian(newSpan);
    }

    public static float Inverted(this float value)
    {
        return BitConverter.UInt32BitsToSingle(~BitConverter.SingleToUInt32Bits(value));
    }

    public static double Inverted(this double value)
    {
        return BitConverter.UInt64BitsToDouble(~BitConverter.DoubleToUInt64Bits(value));
    }

    public static float And(this float left, float right)
    {
        return BitConverter.UInt32BitsToSingle(BitConverter.SingleToUInt32Bits(left) & BitConverter.SingleToUInt32Bits(right));
    }

    public static double And(this double left, double right)
    {
        return BitConverter.UInt64BitsToDouble(BitConverter.DoubleToUInt64Bits(left) & BitConverter.DoubleToUInt64Bits(right));
    }

    public static float Andn(this float left, float right)
    {
        return BitConverter.UInt32BitsToSingle(BitConverter.SingleToUInt32Bits(left) & ~BitConverter.SingleToUInt32Bits(right));
    }

    public static double Andn(this double left, double right)
    {
        return BitConverter.UInt64BitsToDouble(BitConverter.DoubleToUInt64Bits(left) & ~BitConverter.DoubleToUInt64Bits(right));
    }

    public static float Or(this float left, float right)
    {
        return BitConverter.UInt32BitsToSingle(BitConverter.SingleToUInt32Bits(left) | BitConverter.SingleToUInt32Bits(right));
    }

    public static double Or(this double left, double right)
    {
        return BitConverter.UInt64BitsToDouble(BitConverter.DoubleToUInt64Bits(left) | BitConverter.DoubleToUInt64Bits(right));
    }

    public static float Xor(this float left, float right)
    {
        return BitConverter.UInt32BitsToSingle(BitConverter.SingleToUInt32Bits(left) ^ BitConverter.SingleToUInt32Bits(right));
    }

    public static double Xor(this double left, double right)
    {
        return BitConverter.UInt64BitsToDouble(BitConverter.DoubleToUInt64Bits(left) ^ BitConverter.DoubleToUInt64Bits(right));
    }
}
