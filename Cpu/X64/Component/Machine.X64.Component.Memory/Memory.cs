using System.Buffers.Binary;
using System.Runtime.Intrinsics;

namespace Machine.X64.Component;

/// <summary>
/// Manages CPU memory.
/// </summary>
public sealed class Memory
{
    private readonly byte[] _data;

    /// <summary>
    /// Initializes a new instance of the <see cref="Memory"/> class.
    /// </summary>
    /// <param name="data">Raw memory data</param>
    public Memory(byte[] data)
    {
        _data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Memory"/> class.
    /// </summary>
    /// <param name="size">Memory size</param>
    public Memory(int size)
    {
        _data = new byte[size];
    }

    /// <summary>
    /// The LDT selector.
    /// </summary>
    public ushort LDTSelector { get; set; }

    /// <summary>
    /// The GDT size.
    /// </summary>
    public uint GDTSize { get; set; }

    /// <summary>
    /// The IDT address.
    /// </summary>
    public uint IDTAddress { get; set; }

    /// <summary>
    /// Reads <see cref="ushort"/> starting at direct offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <returns><see cref="ushort"/></returns>
    public ushort ReadUInt16(ulong pos)
    {
        byte b2 = this[pos];
        byte b1 = this[pos + 1];
        return (ushort)(b2 | b1 << 8);
    }

    /// <summary>
    /// Reads <see cref="uint"/> starting at direct offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <returns><see cref="uint"/></returns>
    public uint ReadUInt32(ulong pos)
    {
        byte b4 = this[pos];
        byte b3 = this[pos + 1];
        byte b2 = this[pos + 2];
        byte b1 = this[pos + 3];

        return (uint)(b4 | b3 << 8 | b2 << 16 | b1 << 24);
    }

    /// <summary>
    /// Reads <see cref="ulong"/> starting at direct offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <returns><see cref="ulong"/></returns>
    public ulong ReadUInt64(ulong pos)
    {
        byte b8 = this[pos];
        byte b7 = this[pos + 1];
        byte b6 = this[pos + 2];
        byte b5 = this[pos + 3];
        byte b4 = this[pos + 4];
        byte b3 = this[pos + 5];
        byte b2 = this[pos + 6];
        byte b1 = this[pos + 7];

        return b8 |
           (ulong)b7 << 8 |
           (ulong)b6 << 16 |
           (ulong)b5 << 24 |
           (ulong)b4 << 32 |
           (ulong)b3 << 40 |
           (ulong)b2 << 48 |
           (ulong)b1 << 56;
    }

    /// <summary>
    /// Writes <see cref="ushort"/> starting at direct memory offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <param name="value">Value to write.</param>
    public void WriteUInt16(ulong pos, ushort value)
    {
        byte[] array = BitConverter.GetBytes(BinaryPrimitives.ReverseEndianness(value));
        this[pos] = array[1];
        this[pos + 1] = array[0];
    }

    /// <summary>
    /// Writes <see cref="uint"/> starting at direct memory offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <param name="value">Value to write.</param>
    public void WriteUInt32(ulong pos, uint value)
    {
        byte[] array = BitConverter.GetBytes(BinaryPrimitives.ReverseEndianness(value));
        this[pos] = array[3];
        this[pos + 1] = array[2];
        this[pos + 2] = array[1];
        this[pos + 3] = array[0];
    }

    /// <summary>
    /// Writes <see cref="ulong"/> starting at direct memory offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <param name="value">Value to write.</param>
    public void WriteUInt64(ulong pos, ulong value)
    {
        byte[] array = BitConverter.GetBytes(BinaryPrimitives.ReverseEndianness(value));
        this[pos] = array[7];
        this[pos + 7] = array[6];
        this[pos + 6] = array[5];
        this[pos + 5] = array[4];
        this[pos + 4] = array[3];
        this[pos + 3] = array[2];
        this[pos + 2] = array[1];
        this[pos + 1] = array[0];
    }

    /// <summary>
    /// Reads a descriptor starting at direct memory offset <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Offset.</param>
    /// <returns><see cref="Descriptor"/></returns>
    public Descriptor GetDescriptor(ulong pos)
    {
        return Descriptor.FromUInt64(ReadUInt64(pos));
    }

    /// <summary>
    /// Returns the descriptor at segment.
    /// </summary>
    /// <param name="segment">Segment.</param>
    /// <returns>Descriptor.</returns>
    public Descriptor GetDescriptorAtSegment(uint segment)
    {
        uint selectorIndex = segment >> 3;
        uint baseAddress;

        if ((segment & 4) != 0)
        {
            var ldtDescriptor = SegmentDescriptor.FromBase(GetDescriptor(LDTSelector & 0xFFF8u));
            baseAddress = ldtDescriptor.BaseAddress;
        }
        else
        {
            baseAddress = GDTSize;
        }

        ulong value = ReadUInt64(baseAddress + (selectorIndex * 8u));
        Descriptor descriptor = Descriptor.FromUInt64(value);
        return descriptor;
    }

    /// <summary>
    /// Returns the interrupt descriptor.
    /// </summary>
    /// <param name="interrupt">Interrupt.</param>
    /// <returns>Descriptor.</returns>
    public Descriptor GetInterruptDescriptor(byte interrupt)
    {
        return Descriptor.FromUInt64(ReadUInt64(IDTAddress + (interrupt * 8u)));
    }

    /// <summary>
    /// Reads 64 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 64 bytes.</returns>
    public ReadOnlySpan<byte> Read64Bytes(ulong pos)
    {
        return new ReadOnlySpan<byte>(
            [
                this[pos],
                this[pos + 1],
                this[pos + 2],
                this[pos + 3],
                this[pos + 4],
                this[pos + 5],
                this[pos + 6],
                this[pos + 7],
                this[pos + 8],
                this[pos + 9],
                this[pos + 10],
                this[pos + 11],
                this[pos + 12],
                this[pos + 13],
                this[pos + 14],
                this[pos + 15],
                this[pos + 16],
                this[pos + 17],
                this[pos + 18],
                this[pos + 19],
                this[pos + 20],
                this[pos + 21],
                this[pos + 22],
                this[pos + 23],
                this[pos + 24],
                this[pos + 25],
                this[pos + 26],
                this[pos + 27],
                this[pos + 28],
                this[pos + 29],
                this[pos + 30],
                this[pos + 31],
                this[pos + 32],
                this[pos + 33],
                this[pos + 34],
                this[pos + 35],
                this[pos + 36],
                this[pos + 37],
                this[pos + 38],
                this[pos + 39],
                this[pos + 40],
                this[pos + 41],
                this[pos + 42],
                this[pos + 43],
                this[pos + 44],
                this[pos + 45],
                this[pos + 46],
                this[pos + 47],
                this[pos + 48],
                this[pos + 49],
                this[pos + 50],
                this[pos + 51],
                this[pos + 52],
                this[pos + 53],
                this[pos + 54],
                this[pos + 55],
                this[pos + 56],
                this[pos + 57],
                this[pos + 58],
                this[pos + 59],
                this[pos + 60],
                this[pos + 61],
                this[pos + 62],
                this[pos + 63]
            ]
        );
    }

    /// <summary>
    /// Reads 32 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 32 bytes.</returns>
    public ReadOnlySpan<byte> Read32Bytes(ulong pos)
    {
        return new ReadOnlySpan<byte>(
            [
                this[pos],
                this[pos + 1],
                this[pos + 2],
                this[pos + 3],
                this[pos + 4],
                this[pos + 5],
                this[pos + 6],
                this[pos + 7],
                this[pos + 8],
                this[pos + 9],
                this[pos + 10],
                this[pos + 11],
                this[pos + 12],
                this[pos + 13],
                this[pos + 14],
                this[pos + 15],
                this[pos + 16],
                this[pos + 17],
                this[pos + 18],
                this[pos + 19],
                this[pos + 20],
                this[pos + 21],
                this[pos + 22],
                this[pos + 23],
                this[pos + 24],
                this[pos + 25],
                this[pos + 26],
                this[pos + 27],
                this[pos + 28],
                this[pos + 29],
                this[pos + 30],
                this[pos + 31]
            ]
        );
    }

    /// <summary>
    /// Reads 16 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 16 bytes.</returns>
    public ReadOnlySpan<byte> Read16Bytes(ulong pos)
    {
        return new ReadOnlySpan<byte>(
            [
                this[pos],
                this[pos + 1],
                this[pos + 2],
                this[pos + 3],
                this[pos + 4],
                this[pos + 5],
                this[pos + 6],
                this[pos + 7],
                this[pos + 8],
                this[pos + 9],
                this[pos + 10],
                this[pos + 11],
                this[pos + 12],
                this[pos + 13],
                this[pos + 14],
                this[pos + 15]
            ]
        );
    }

    /// <summary>
    /// Reads 8 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 8 bytes.</returns>
    public ReadOnlySpan<byte> Read8Bytes(ulong pos)
    {
        return new ReadOnlySpan<byte>(
            [
                this[pos],
                this[pos + 1],
                this[pos + 2],
                this[pos + 3],
                this[pos + 4],
                this[pos + 5],
                this[pos + 6],
                this[pos + 7]
            ]
        );
    }

    /// <summary>
    /// Reads 10 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 10 bytes.</returns>
    public ReadOnlySpan<byte> Read10Bytes(ulong pos)
    {
        return new ReadOnlySpan<byte>(
            [
                this[pos],
                this[pos + 1],
                this[pos + 2],
                this[pos + 3],
                this[pos + 4],
                this[pos + 5],
                this[pos + 6],
                this[pos + 7],
                this[pos + 8],
                this[pos + 9]
            ]
        );
    }

    /// <summary>
    /// Reads a Vector64 (Little endian) from given position in memory.
    /// </summary>
    /// <param name="pos">Memory address.</param>
    /// <returns>A vector 64.</returns>
    public Vector64<float> ReadBinaryVector64(ulong pos)
    {
        ulong value = BinaryPrimitives.ReadUInt64LittleEndian(Read8Bytes(pos));
        return Vector64.Create<float>(value);
    }

    /// <summary>
    /// Reads a Vector128 (Little endian) from given position in memory.
    /// </summary>
    /// <param name="pos">Memory address.</param>
    /// <returns>A vector 128.</returns>
    public Vector128<float> ReadBinaryVector128(ulong pos)
    {
        return Vector128.Create(ReadBinaryVector64(pos), ReadBinaryVector64(pos + 8));
    }

    /// <summary>
    /// Reads a Vector256 (Little endian) from given position in memory.
    /// </summary>
    /// <param name="pos">Memory address.</param>
    /// <returns>A vector 256.</returns>
    public Vector256<float> ReadBinaryVector256(ulong pos)
    {
        return Vector256.Create(ReadBinaryVector128(pos), ReadBinaryVector128(pos + 8));
    }

    /// <summary>
    /// Reads a Vector512 (Little endian) from given position in memory.
    /// </summary>
    /// <param name="pos">Memory address.</param>
    /// <returns>A vector 512.</returns>
    public Vector512<float> ReadBinaryVector512(ulong pos)
    {
        return Vector512.Create(ReadBinaryVector256(pos), ReadBinaryVector256(pos + 8));
    }

    /// <summary>
    /// Writes a binary vector 128.
    /// </summary>
    /// <typeparam name="T">Type of the vector.</typeparam>
    /// <param name="pos">Offset where it should be written.</param>
    /// <param name="value">The vector to write.</param>
    public void WriteBinaryVector128<T>(ulong pos, Vector128<T> value)
    {
        Vector128<byte> normalizedVector = value.As<T, byte>();
        this[pos + 15] = normalizedVector[0];
        this[pos + 14] = normalizedVector[1];
        this[pos + 13] = normalizedVector[2];
        this[pos + 12] = normalizedVector[3];
        this[pos + 11] = normalizedVector[4];
        this[pos + 10] = normalizedVector[5];
        this[pos + 9] = normalizedVector[6];
        this[pos + 8] = normalizedVector[7];
        this[pos + 7] = normalizedVector[8];
        this[pos + 6] = normalizedVector[9];
        this[pos + 5] = normalizedVector[10];
        this[pos + 4] = normalizedVector[11];
        this[pos + 3] = normalizedVector[12];
        this[pos + 2] = normalizedVector[13];
        this[pos + 1] = normalizedVector[14];
        this[pos + 0] = normalizedVector[15];
    }

    /// <summary>
    /// Writes a binary vector 256.
    /// </summary>
    /// <typeparam name="T">Type of the vector.</typeparam>
    /// <param name="pos">Offset where it should be written.</param>
    /// <param name="value">The vector to write.</param>
    public void WriteBinaryVector256<T>(ulong pos, Vector256<T> value)
    {
        Vector256<byte> normalizedVector = value.As<T, byte>();
        this[pos + 31] = normalizedVector[0];
        this[pos + 30] = normalizedVector[1];
        this[pos + 29] = normalizedVector[2];
        this[pos + 28] = normalizedVector[3];
        this[pos + 27] = normalizedVector[4];
        this[pos + 26] = normalizedVector[5];
        this[pos + 25] = normalizedVector[6];
        this[pos + 24] = normalizedVector[7];
        this[pos + 23] = normalizedVector[8];
        this[pos + 22] = normalizedVector[9];
        this[pos + 21] = normalizedVector[10];
        this[pos + 20] = normalizedVector[11];
        this[pos + 19] = normalizedVector[12];
        this[pos + 18] = normalizedVector[13];
        this[pos + 17] = normalizedVector[14];
        this[pos + 16] = normalizedVector[15];
        this[pos + 15] = normalizedVector[16];
        this[pos + 14] = normalizedVector[17];
        this[pos + 13] = normalizedVector[18];
        this[pos + 12] = normalizedVector[19];
        this[pos + 11] = normalizedVector[20];
        this[pos + 10] = normalizedVector[21];
        this[pos + 9] = normalizedVector[22];
        this[pos + 8] = normalizedVector[23];
        this[pos + 7] = normalizedVector[24];
        this[pos + 6] = normalizedVector[25];
        this[pos + 5] = normalizedVector[26];
        this[pos + 4] = normalizedVector[27];
        this[pos + 3] = normalizedVector[28];
        this[pos + 2] = normalizedVector[29];
        this[pos + 1] = normalizedVector[30];
        this[pos] = normalizedVector[31];
    }

    /// <summary>
    /// Writes a binary vector 512.
    /// </summary>
    /// <typeparam name="T">Type of the vector.</typeparam>
    /// <param name="pos">Offset where it should be written.</param>
    /// <param name="value">The vector to write.</param>
    public void WriteBinaryVector512<T>(ulong pos, Vector512<T> value)
    {
        Vector512<byte> normalizedVector = value.As<T, byte>();
        this[pos + 63] = normalizedVector[0];
        this[pos + 62] = normalizedVector[1];
        this[pos + 61] = normalizedVector[2];
        this[pos + 60] = normalizedVector[3];
        this[pos + 59] = normalizedVector[4];
        this[pos + 58] = normalizedVector[5];
        this[pos + 57] = normalizedVector[6];
        this[pos + 56] = normalizedVector[7];
        this[pos + 55] = normalizedVector[8];
        this[pos + 54] = normalizedVector[9];
        this[pos + 53] = normalizedVector[10];
        this[pos + 52] = normalizedVector[11];
        this[pos + 51] = normalizedVector[12];
        this[pos + 50] = normalizedVector[13];
        this[pos + 49] = normalizedVector[14];
        this[pos + 48] = normalizedVector[15];
        this[pos + 47] = normalizedVector[16];
        this[pos + 46] = normalizedVector[17];
        this[pos + 45] = normalizedVector[18];
        this[pos + 44] = normalizedVector[19];
        this[pos + 43] = normalizedVector[20];
        this[pos + 42] = normalizedVector[21];
        this[pos + 41] = normalizedVector[22];
        this[pos + 40] = normalizedVector[23];
        this[pos + 39] = normalizedVector[24];
        this[pos + 38] = normalizedVector[25];
        this[pos + 37] = normalizedVector[26];
        this[pos + 36] = normalizedVector[27];
        this[pos + 35] = normalizedVector[28];
        this[pos + 34] = normalizedVector[29];
        this[pos + 33] = normalizedVector[30];
        this[pos + 32] = normalizedVector[31];
        this[pos + 31] = normalizedVector[32];
        this[pos + 30] = normalizedVector[33];
        this[pos + 29] = normalizedVector[34];
        this[pos + 28] = normalizedVector[35];
        this[pos + 27] = normalizedVector[36];
        this[pos + 26] = normalizedVector[37];
        this[pos + 25] = normalizedVector[38];
        this[pos + 24] = normalizedVector[39];
        this[pos + 23] = normalizedVector[40];
        this[pos + 22] = normalizedVector[41];
        this[pos + 21] = normalizedVector[42];
        this[pos + 20] = normalizedVector[43];
        this[pos + 19] = normalizedVector[44];
        this[pos + 18] = normalizedVector[45];
        this[pos + 17] = normalizedVector[46];
        this[pos + 16] = normalizedVector[47];
        this[pos + 15] = normalizedVector[48];
        this[pos + 14] = normalizedVector[49];
        this[pos + 13] = normalizedVector[50];
        this[pos + 12] = normalizedVector[51];
        this[pos + 11] = normalizedVector[52];
        this[pos + 10] = normalizedVector[53];
        this[pos + 9] = normalizedVector[54];
        this[pos + 8] = normalizedVector[55];
        this[pos + 7] = normalizedVector[56];
        this[pos + 6] = normalizedVector[57];
        this[pos + 5] = normalizedVector[58];
        this[pos + 4] = normalizedVector[59];
        this[pos + 3] = normalizedVector[60];
        this[pos + 2] = normalizedVector[61];
        this[pos + 1] = normalizedVector[62];
        this[pos] = normalizedVector[63];
    }

    /// <summary>
    /// Writes a binary vector 64.
    /// </summary>
    /// <typeparam name="T">Type of the vector.</typeparam>
    /// <param name="pos">Offset where it should be written.</param>
    /// <param name="value">The vector to write.</param>
    public void WriteBinaryVector64<T>(ulong pos, Vector64<T> value)
    {
        Vector64<byte> normalizedVector = value.As<T, byte>();
        this[pos + 7] = normalizedVector[0];
        this[pos + 6] = normalizedVector[1];
        this[pos + 5] = normalizedVector[2];
        this[pos + 4] = normalizedVector[3];
        this[pos + 3] = normalizedVector[4];
        this[pos + 2] = normalizedVector[5];
        this[pos + 1] = normalizedVector[6];
        this[pos] = normalizedVector[7];
    }

    /// <summary>
    /// Reads 4 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 4 bytes.</returns>
    public ReadOnlySpan<byte> Read4Bytes(ulong pos)
    {
        return new(
            [
                this[pos],
                this[pos + 1],
                this[pos + 2],
                this[pos + 3]
            ]
        );
    }

    /// <summary>
    /// Reads 2 bytes from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A read-only span that represents 2 bytes.</returns>
    public ReadOnlySpan<byte> Read2Bytes(ulong pos)
    {
        return new(
            [
                this[pos],
                this[pos + 1]
            ]
        );
    }

    /// <summary>
    /// Reads a 32-bit binary vector as 64-bit one from given offset.
    /// </summary>
    /// <typeparam name="T">Vector type</typeparam>
    /// <param name="pos">Offset</param>
    /// <returns><see cref="Vector64{T}"/></returns>
    public Vector64<T> ReadBinaryVector32<T>(ulong pos)
    {
        return Vector64.Create(Read4Bytes(pos)).As<byte, T>();
    }

    /// <summary>
    /// Reads a 16-bit binary vector as 64-bit one from given offset.
    /// </summary>
    /// <typeparam name="T">Vector type</typeparam>
    /// <param name="pos">Offset</param>
    /// <returns><see cref="Vector64{T}"/></returns>
    public Vector64<T> ReadBinaryVector16<T>(ulong pos)
    {
        return Vector64.Create([.. Read2Bytes(pos), (byte)0, (byte)0]).As<byte, T>();
    }

    /// <summary>
    /// Reads a floating-point integer from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A floating-point integer.</returns>
    public float ReadSingle(ulong pos)
    {
        return BinaryPrimitives.ReadSingleLittleEndian(Read4Bytes(pos));
    }

    /// <summary>
    /// Reads a double-precision floating-point integer from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A double-precision floating-point integer.</returns>
    public double ReadDouble(ulong pos)
    {
        return BinaryPrimitives.ReadDoubleLittleEndian(Read4Bytes(pos));
    }

    /// <summary>
    /// Writes a single-precision floating-point integer to memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address (offset).</param>
    /// <param name="value">Single-precision floating-point integer to write.</param>
    public void WriteSingle(ulong pos, float value)
    {
        var bytes = new Span<byte>(new byte[4]); // float is 4 bytes in size
        BinaryPrimitives.WriteSingleLittleEndian(bytes, value);
        this[pos + 3] = bytes[0];
        this[pos + 2] = bytes[1];
        this[pos + 1] = bytes[2];
        this[pos] = bytes[3];
    }

    /// <summary>
    /// Writes a double-precision floating-point integer to memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address (offset).</param>
    /// <param name="value">Double-precision floating-point integer to write.</param>
    public void WriteDouble(ulong pos, double value)
    {
        var bytes = new Span<byte>(new byte[8]); // double is 8 bytes in size
        BinaryPrimitives.WriteDoubleBigEndian(bytes, value);
        this[pos + 7] = bytes[0];
        this[pos + 6] = bytes[1];
        this[pos + 5] = bytes[2];
        this[pos + 4] = bytes[3];
        this[pos + 3] = bytes[4];
        this[pos + 2] = bytes[5];
        this[pos + 1] = bytes[6];
        this[pos] = bytes[7];
    }

    /// <summary>
    /// Writes a half-precision floating-point integer to memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address (offset).</param>
    /// <param name="value">Half-precision floating-point integer to write.</param>
    public void WriteHalf(ulong pos, Half value)
    {
        var bytes = new Span<byte>(new byte[2]); // Half is 2 bytes in size
        BinaryPrimitives.WriteHalfBigEndian(bytes, value);
        this[pos + 1] = bytes[0];
        this[pos] = bytes[1];
    }

    /// <summary>
    /// Reads a half-precision floating-point integer from memory starting at <paramref name="pos"/>.
    /// </summary>
    /// <param name="pos">Base address.</param>
    /// <returns>A half-precision floating-point integer.</returns>
    public Half ReadHalf(ulong pos)
    {
        return BinaryPrimitives.ReadHalfBigEndian(Read2Bytes(pos));
    }

    /// <summary>
    /// Gets or sets a single byte from memory at given offset.
    /// </summary>
    /// <param name="index">Offset.</param>
    /// <returns>A byte at given offset.</returns>
    public byte this[ulong index]
    {
        get => _data[index];
        set => _data[index] = value;
    }
}
