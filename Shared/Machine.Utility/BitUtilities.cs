using System.Runtime.CompilerServices;

namespace Machine.Utility;

public static class BitUtilities
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong AddFirstNBits(ulong a, ulong b, int n)
    {
        ulong mask = (1UL << n) - 1;
        ulong x = a & mask;
        ulong y = b & mask;
        ulong sum = x + y;
        ulong result = sum & mask;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong LeftShiftFirstNBits(ulong value, int n)
    {
        ulong mask = (1UL << n) - 1;
        ulong shiftedBits = (value & mask) << 1;
        return shiftedBits;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong RightShiftFirstNBits(ulong value, int n)
    {
        ulong mask = (1UL << n) - 1;
        ulong result = value >> n & mask;
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(byte value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(ulong value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(float value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.SingleToInt32Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(double value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.DoubleToInt64Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(short value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(int value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(long value, int bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    // IsBitSet: byte
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(byte value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(ulong value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(float value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.SingleToInt32Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(double value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.DoubleToInt64Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(short value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(int value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(long value, byte bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    // IsBitSet: ushort
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(byte value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(ulong value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(float value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.SingleToInt32Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(double value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.DoubleToInt64Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(short value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(int value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(long value, ushort bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = littleEndian ? bitIndex : bitIndexMax - bitIndex;
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    // IsBitSet: uint
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(byte value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : bitIndexMax - bitIndex);
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(ulong value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : bitIndexMax - bitIndex);
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(float value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.SingleToInt32Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(double value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.DoubleToInt64Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(short value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : bitIndexMax - bitIndex);
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(int value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : bitIndexMax - bitIndex);
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(long value, uint bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : bitIndexMax - bitIndex);
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    // IsBitSet: ulong
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(byte value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : (ulong)bitIndexMax - bitIndex);
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(ulong value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : (ulong)bitIndexMax - bitIndex);
        return (value & 1UL << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(float value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.SingleToInt32Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(double value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        return IsBitSet(BitConverter.DoubleToInt64Bits(value), bitIndex, littleEndian, bitIndexMax);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(short value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : (ulong)bitIndexMax - bitIndex);
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(int value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : (ulong)bitIndexMax - bitIndex);
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsBitSet(long value, ulong bitIndex, bool littleEndian = false, int bitIndexMax = 63)
    {
        int littleEndianBitIndex = (int)(littleEndian ? bitIndex : (ulong)bitIndexMax - bitIndex);
        return (value & 1 << littleEndianBitIndex) != 0;
    }

    public static ulong SetBit(ulong number, int bitPosition, bool value)
    {
        if (bitPosition < 0 || bitPosition > 63)
            throw new ArgumentOutOfRangeException(nameof(bitPosition), "Bit index must be between 0 and 63");

        ulong mask = 1UL << bitPosition;

        if (value)
        {
            number |= mask;
        }
        else
        {
            number &= ~mask;
        }

        return number;
    }

    public static void SetBit(ref ulong number, int bitPosition, bool value)
    {
        if (bitPosition < 0 || bitPosition > 63)
            throw new ArgumentOutOfRangeException(nameof(bitPosition), "Bit index must be between 0 and 63");

        ulong mask = 1UL << bitPosition;

        if (value)
        {
            number |= mask;
        }
        else
        {
            number &= ~mask;
        }
    }

    public static void SetBit(ref uint number, int bitPosition, bool value)
    {
        if (bitPosition < 0 || bitPosition > 31)
            throw new ArgumentOutOfRangeException(nameof(bitPosition), "Bit index must be between 0 and 31");

        uint mask = 1u << bitPosition;

        if (value)
        {
            number |= mask;
        }
        else
        {
            number &= ~mask;
        }
    }

    public static int RotateLeft(int value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value << count) | (value >> (bits - count));
    }

    public static uint RotateLeft(uint value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value << count) | (value >> (bits - count));
    }

    public static long RotateLeft(long value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value << count) | (value >> (bits - count));
    }

    public static ulong RotateLeft(ulong value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value << count) | (value >> (bits - count));
    }

    public static Int128 RotateLeft(Int128 value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value << count) | (value >> (bits - count));
    }

    public static int RotateRight(int value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value >> count) | (value << (bits - count));
    }

    public static uint RotateRight(uint value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value >> count) | (value << (bits - count));
    }

    public static long RotateRight(long value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value >> count) | (value << (bits - count));
    }

    public static ulong RotateRight(ulong value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value >> count) | (value << (bits - count));
    }

    public static Int128 RotateRight(Int128 value, int count)
    {
        int bits = sizeof(int) * 8;
        count %= bits;
        return (value >> count) | (value << (bits - count));
    }

    public static uint GetLower32Bits(ulong value)
    {
        return (uint)(value & 0xFFFFFFFF);
    }

    public static uint GetUpper32Bits(ulong value)
    {
        return (uint)(value >> 32);
    }

    public static void SetLower32Bits(ref ulong value, uint by)
    {
        value = (value & 0xFFFFFFFF00000000) | by;
    }

    public static void SetLower16Bits(ref uint value, ushort by)
    {
        value = (uint)((value & 0xFFFF0000) | by);
    }

    public static uint SetLower16Bits(uint value, ushort by)
    {
        return (value & 0xFFFF0000) | by;
    }

    public static void SetUpper32Bits(ref ulong value, uint by)
    {
        value = (value & 0x00000000FFFFFFFF) | ((ulong)by << 32);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong CreateUInt64(uint high, uint low)
    {
        return ((ulong)high << 32) | low;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetLower8Bits(ref uint value, byte by)
    {
        value = (value & 0xFFFFFF00) | by;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint SetLower8Bits(uint value, byte by)
    {
        return (value & 0xFFFFFF00) | by;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetLower8Bits(ref ulong value, byte by)
    {
        value = (value & 0xFFFFFFFFFFFFFF00) | by;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong SetLower8Bits(ulong value, byte by)
    {
        return (value & 0xFFFFFFFFFFFFFF00) | by;
    }

    public static ushort GetLower16Bits(uint value)
    {
        return (ushort)(value & 0xFFFF);
    }

    public static byte GetLower8Bits(uint value)
    {
        return (byte)(GetLower16Bits(value) & 0xFF);
    }

    public static byte GetLower8Bits(ulong value)
    {
        return (byte)(GetLower16Bits(value) & 0xFF);
    }

    public static ushort GetLower16Bits(ulong value)
    {
        return GetLower16Bits(GetLower32Bits(value));
    }

    public static ushort GetUpper16Bits(uint value)
    {
        return (ushort)(value >> 16);
    }

    public static short GetUpper16Bits(int value)
    {
        return (short)(value >> 16);
    }

    public static uint SetMostSignificantBit(uint value)
    {
        return value | (1u << 31);
    }

    public static ushort SetMostSignificantBit(ushort value)
    {
        return (ushort)(value | (1 << 15));
    }

    public static ulong SetMostSignificantBit(ulong value)
    {
        return value | (1uL << 63);
    }

    public static int PopCount(byte value)
    {
        int count = 0;
        while (value != 0)
        {
            count += value & 1;
            value >>= 1;
        }
        return count;
    }

    public static int PopCount(ushort value)
    {
        int count = 0;
        while (value != 0)
        {
            count += value & 1;
            value >>= 1;
        }
        return count;
    }

    public static int PopCount(ulong value)
    {
        int count = 0;
        while (value != 0)
        {
            count += (int)(value & 1);
            value >>= 1;
        }
        return count;
    }

    public static byte Complement(byte value, byte bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static ushort Complement(ushort value, byte bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static uint Complement(uint value, byte bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static ulong Complement(ulong value, byte bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static byte Complement(byte value, ushort bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static ushort Complement(ushort value, ushort bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static uint Complement(uint value, ushort bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static ulong Complement(ulong value, ushort bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << bitIndex)
            : (byte)(value & ~(1u << bitIndex));
    }

    public static byte Complement(byte value, uint bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static ushort Complement(ushort value, uint bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static uint Complement(uint value, uint bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static ulong Complement(ulong value, uint bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static byte Complement(byte value, ulong bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static ushort Complement(ushort value, ulong bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static uint Complement(uint value, ulong bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static ulong Complement(ulong value, ulong bitIndex)
    {
        return !IsBitSet(value, bitIndex)
            ? (byte)(value | 1u << (int)bitIndex)
            : (byte)(value & ~(1u << (int)bitIndex));
    }

    public static byte Zero(byte value, byte bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(byte value, ushort bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(byte value, uint bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(byte value, ulong bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ushort value, byte bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ushort value, ushort bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ushort value, uint bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ushort value, ulong bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(uint value, byte bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(uint value, ushort bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(uint value, uint bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(uint value, ulong bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ulong value, byte bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ulong value, ushort bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ulong value, uint bitIndex) => (byte)(value & ~(1u << (int)bitIndex));
    public static byte Zero(ulong value, ulong bitIndex) => (byte)(value & ~(1u << (int)bitIndex));

    public static byte One(byte value, byte bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(byte value, ushort bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(byte value, uint bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(byte value, ulong bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ushort value, byte bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ushort value, ushort bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ushort value, uint bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ushort value, ulong bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(uint value, byte bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(uint value, ushort bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(uint value, uint bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(uint value, ulong bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ulong value, byte bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ulong value, ushort bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ulong value, uint bitIndex) => (byte)(value | 1u << (int)bitIndex);
    public static byte One(ulong value, ulong bitIndex) => (byte)(value | 1u << (int)bitIndex);

    public static ulong SignExtend(byte value)
    {
        return (ulong)(sbyte)value;
    }

    public static ulong SignExtend(ushort value)
    {
        return (ulong)(short)value;
    }

    public static uint CreateUInt32(byte value1, byte value2, byte value3, byte value4)
    {
        return (uint)(value1 << 24 | value2 << 16 | value3 << 8 | value4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ByteOrderSwap(ushort value)
    {
        return (ushort)((value << 8) | (value >> 8));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ByteOrderSwap(uint value)
    {
        return ((value << 24) & 0xFF000000) | ((value << 8) & 0x00FF0000) | ((value >> 8) & 0x0000FF00) | ((value >> 24) & 0x000000FF);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ByteOrderSwap(ulong value)
    {
        return ((value << 56) & 0xFF00000000000000UL) | ((value << 40) & 0x00FF000000000000UL) | ((value << 24) & 0x0000FF0000000000UL) | ((value << 8) & 0x000000FF00000000UL) | ((value >> 8) & 0x00000000FF000000UL) | ((value >> 24) & 0x0000000000FF0000UL) | ((value >> 40) & 0x000000000000FF00UL) | ((value >> 56) & 0x00000000000000FFUL);
    }
}
