using System.Diagnostics.CodeAnalysis;

namespace Machine.Firmware.Uefi;

/// <summary>
/// The XSDP Table for ACPI 2.0 and later.
/// </summary>
public sealed record AcpiXsdpTable
{
    /// <summary>
    /// Signature of the XSDP. This is eight bytes and must have a value of "RSD PTR ".
    /// </summary>
    public ReadOnlyMemory<byte> Signature { get; init; }

    /// <summary>
    /// Checksum of the first 20 bytes of the XSDP.
    /// </summary>
    public byte Checksum { get; init; }

    /// <summary>
    /// OEM Identifier. This is always 6 bytes.
    /// </summary>
    public ReadOnlyMemory<byte> OemId { get; init; }

    /// <summary>
    /// If this is 0, this refers to ACPI 1.0. Otherwise, if it's 2, it refers to ACPI 2.0+. Values can't
    /// be 1, or something greater than 2.
    /// </summary>
    public byte Revision { get; init; }

    /// <summary>
    /// Address of the RSDT. Deprecated in ACPI 2.0.
    /// </summary>
    public uint RsdtAddress { get; init; }

    /// <summary>
    /// Size of the entire table since offset 0 to the end.
    /// </summary>
    public uint Length { get; init; }

    /// <summary>
    /// Address of the XSDT structure.
    /// </summary>
    public ulong XsdtAddress { get; init; }

    /// <summary>
    /// Extended checksum.
    /// </summary>
    public byte ExtendedChecksum { get; init; }

    /// <summary>
    /// 3 bytes of reserved data.
    /// </summary>
    public ReadOnlyMemory<byte> Reserved { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcpiXsdpTable"/> class.
    /// </summary>
    /// <param name="signature">Signature of the XSDP. This is eight bytes and must have a value of "RSD PTR ".</param>
    /// <param name="checksum">Checksum of the first 20 bytes of the XSDP.</param>
    /// <param name="oemId">OEM Identifier. This is always 6 bytes.</param>
    /// <param name="revision">If this is 0, this refers to ACPI 1.0. Otherwise, if it's 2, it refers to ACPI 2.0+. Values can't
    /// be 1, or something greater than 2.</param>
    /// <param name="rsdtAddress">Address of the RSDT. Deprecated in ACPI 2.0.</param>
    /// <param name="length">Size of the entire table since offset 0 to the end.</param>
    /// <param name="xsdtAddress">Address of the XSDT structure.</param>
    /// <param name="extendedChecksum">Extended checksum.</param>
    /// <param name="reserved">3 bytes of reserved data.</param>
    public AcpiXsdpTable(
        ReadOnlyMemory<byte> signature, byte checksum, ReadOnlyMemory<byte> oemId, byte revision, uint rsdtAddress, 
        uint length, ulong xsdtAddress, byte extendedChecksum, ReadOnlyMemory<byte> reserved)
    {
        this.Signature = signature;
        this.Checksum = checksum;
        this.OemId = oemId;
        this.Revision = revision;
        this.RsdtAddress = rsdtAddress;
        this.Length = length;
        this.XsdtAddress = xsdtAddress;
        this.ExtendedChecksum = extendedChecksum;
        this.Reserved = reserved;
    }

    /// <summary>
    /// Writes a binary representation of the ACPI XSDP structure to the given stream.
    /// </summary>
    /// <param name="stream">The result stream where the XSDP structure is written to.</param>
    public void Write(Stream stream)
    {
        byte[] signatureByteArray = this.Signature.ToArray();
        stream.Write(signatureByteArray, 0, signatureByteArray.Length);

        stream.WriteByte(this.Checksum);

        byte[] oemIdArray = this.OemId.ToArray();
        stream.Write(oemIdArray, 0, oemIdArray.Length);

        stream.WriteByte(this.Revision);

        byte[] rsdtAddress = BitConverter.GetBytes(this.RsdtAddress);
        stream.Write(rsdtAddress, 0, rsdtAddress.Length);

        byte[] length = BitConverter.GetBytes(this.Length);
        stream.Write(length, 0, length.Length);

        byte[] xsdtAddress = BitConverter.GetBytes(this.XsdtAddress);
        stream.Write(xsdtAddress, 0, xsdtAddress.Length);

        stream.WriteByte(this.ExtendedChecksum);

        byte[] reserved = this.Reserved.ToArray();
        stream.Write(reserved, 0, reserved.Length);
    }

    /// <summary>
    /// Writes a binary representation of the ACPI XSDP structure to the given stream.
    /// </summary>
    /// <param name="stream">The result stream where the XSDP structure is written to.</param>
    [SuppressMessage("Performance", "CA1835:Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'", Justification = "<Pending>")]
    public async Task WriteAsync(Stream stream)
    {
        byte[] signatureByteArray = this.Signature.ToArray();
        await stream.WriteAsync(signatureByteArray, 0, signatureByteArray.Length);
        
        await stream.WriteAsync([this.Checksum], 0, 1);

        byte[] oemIdArray = this.OemId.ToArray();
        await stream.WriteAsync(oemIdArray, 0, oemIdArray.Length);

        await stream.WriteAsync([this.Revision], 0, 1);

        byte[] rsdtAddress = BitConverter.GetBytes(this.RsdtAddress);
        await stream.WriteAsync(rsdtAddress, 0, rsdtAddress.Length);

        byte[] length = BitConverter.GetBytes(this.Length);
        await stream.WriteAsync(length, 0, length.Length);

        byte[] xsdtAddress = BitConverter.GetBytes(this.XsdtAddress);
        await stream.WriteAsync(xsdtAddress, 0, xsdtAddress.Length);

        await stream.WriteAsync([this.ExtendedChecksum], 0, 1);

        byte[] reserved = this.Reserved.ToArray();
        await stream.WriteAsync(reserved, 0, reserved.Length);
    }

    /// <summary>
    /// Loads an XSDP table from an existing binary reader.
    /// </summary>
    /// <param name="stream">Binary reader</param>
    /// <returns>XSDP table, parsed from its binary representation</returns>
    public static AcpiXsdpTable Load(BinaryReader stream)
    {
        return new AcpiXsdpTable(
            signature: stream.ReadBytes(8).AsMemory(),
            checksum: stream.ReadByte(),
            oemId: stream.ReadBytes(6).AsMemory(),
            revision: stream.ReadByte(),
            rsdtAddress: stream.ReadUInt32(),
            length: stream.ReadUInt32(),
            xsdtAddress: stream.ReadUInt64(),
            extendedChecksum: stream.ReadByte(),
            reserved: stream.ReadBytes(3).AsMemory());
    }
}

/// <summary>
/// The DSDT Table for ACPI.
/// </summary>
public sealed record AcpiDsdtTable
{
    /// <summary>
    /// Signature of the DSDT. This is four bytes and must have a value of "DSDT".
    /// </summary>
    public ReadOnlyMemory<byte> Signature { get; init; }

    /// <summary>
    /// Length of the DSDT table.
    /// </summary>
    public uint Length { get; init; }

    /// <summary>
    /// Revision of the DSDT table.
    /// </summary>
    public byte Revision { get; init; }

    /// <summary>
    /// Checksum of the DSDT table.
    /// </summary>
    public byte Checksum { get; init; }

    /// <summary>
    /// OEM Identifier. This is always 6 bytes.
    /// </summary>
    public ReadOnlyMemory<byte> OemId { get; init; }

    /// <summary>
    /// OEM Table Identifier. This is always 8 bytes.
    /// </summary>
    public ReadOnlyMemory<byte> OemTableId { get; init; }

    /// <summary>
    /// OEM Revision of the DSDT table.
    /// </summary>
    public uint OemRevision { get; init; }

    /// <summary>
    /// ASL Compiler ID. This is always 4 bytes.
    /// </summary>
    public ReadOnlyMemory<byte> AslCompilerId { get; init; }

    /// <summary>
    /// ASL Compiler Revision.
    /// </summary>
    public uint AslCompilerRevision { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcpiDsdtTable"/> class.
    /// </summary>
    /// <param name="signature">Signature of the DSDT. This is four bytes and must have a value of "DSDT".</param>
    /// <param name="length">Length of the DSDT table.</param>
    /// <param name="revision">Revision of the DSDT table.</param>
    /// <param name="checksum">Checksum of the DSDT table.</param>
    /// <param name="oemId">OEM Identifier. This is always 6 bytes.</param>
    /// <param name="oemTableId">OEM Table Identifier. This is always 8 bytes.</param>
    /// <param name="oemRevision">OEM Revision of the DSDT table.</param>
    /// <param name="aslCompilerId">ASL Compiler ID. This is always 4 bytes.</param>
    /// <param name="aslCompilerRevision">ASL Compiler Revision.</param>
    public AcpiDsdtTable(
        ReadOnlyMemory<byte> signature, uint length, byte revision, byte checksum, ReadOnlyMemory<byte> oemId,
        ReadOnlyMemory<byte> oemTableId, uint oemRevision, ReadOnlyMemory<byte> aslCompilerId, uint aslCompilerRevision)
    {
        this.Signature = signature;
        this.Length = length;
        this.Revision = revision;
        this.Checksum = checksum;
        this.OemId = oemId;
        this.OemTableId = oemTableId;
        this.OemRevision = oemRevision;
        this.AslCompilerId = aslCompilerId;
        this.AslCompilerRevision = aslCompilerRevision;
    }

    /// <summary>
    /// Writes a binary representation of the ACPI DSDT structure to the given stream.
    /// </summary>
    /// <param name="stream">The result stream where the DSDT structure is written to.</param>
    public void Write(Stream stream)
    {
        byte[] signatureByteArray = this.Signature.ToArray();
        stream.Write(signatureByteArray, 0, signatureByteArray.Length);

        byte[] length = BitConverter.GetBytes(this.Length);
        stream.Write(length, 0, length.Length);

        stream.WriteByte(this.Revision);
        stream.WriteByte(this.Checksum);

        byte[] oemIdArray = this.OemId.ToArray();
        stream.Write(oemIdArray, 0, oemIdArray.Length);

        byte[] oemTableIdArray = this.OemTableId.ToArray();
        stream.Write(oemTableIdArray, 0, oemTableIdArray.Length);

        byte[] oemRevision = BitConverter.GetBytes(this.OemRevision);
        stream.Write(oemRevision, 0, oemRevision.Length);

        byte[] aslCompilerIdArray = this.AslCompilerId.ToArray();
        stream.Write(aslCompilerIdArray, 0, aslCompilerIdArray.Length);

        byte[] aslCompilerRevision = BitConverter.GetBytes(this.AslCompilerRevision);
        stream.Write(aslCompilerRevision, 0, aslCompilerRevision.Length);
    }

    /// <summary>
    /// Writes a binary representation of the ACPI DSDT structure to the given stream.
    /// </summary>
    /// <param name="stream">The result stream where the DSDT structure is written to.</param>
    [SuppressMessage("Performance", "CA1835:Prefer the 'Memory'-based overloads for 'ReadAsync' and 'WriteAsync'", Justification = "<Pending>")]
    public async Task WriteAsync(Stream stream)
    {
        byte[] signatureByteArray = this.Signature.ToArray();
        await stream.WriteAsync(signatureByteArray, 0, signatureByteArray.Length);

        byte[] length = BitConverter.GetBytes(this.Length);
        await stream.WriteAsync(length, 0, length.Length);

        await stream.WriteAsync([this.Revision], 0, 1);
        await stream.WriteAsync([this.Checksum], 0, 1);

        byte[] oemIdArray = this.OemId.ToArray();
        await stream.WriteAsync(oemIdArray, 0, oemIdArray.Length);

        byte[] oemTableIdArray = this.OemTableId.ToArray();
        await stream.WriteAsync(oemTableIdArray, 0, oemTableIdArray.Length);

        byte[] oemRevision = BitConverter.GetBytes(this.OemRevision);
        await stream.WriteAsync(oemRevision, 0, oemRevision.Length);

        byte[] aslCompilerIdArray = this.AslCompilerId.ToArray();
        await stream.WriteAsync(aslCompilerIdArray, 0, aslCompilerIdArray.Length);

        byte[] aslCompilerRevision = BitConverter.GetBytes(this.AslCompilerRevision);
        await stream.WriteAsync(aslCompilerRevision, 0, aslCompilerRevision.Length);
    }

    /// <summary>
    /// Loads a DSDT table from an existing binary reader.
    /// </summary>
    /// <param name="stream">Binary reader</param>
    /// <returns>DSDT table, parsed from its binary representation</returns>
    public static AcpiDsdtTable Load(BinaryReader stream)
    {
        return new AcpiDsdtTable(
            signature: stream.ReadBytes(4).AsMemory(),
            length: stream.ReadUInt32(),
            revision: stream.ReadByte(),
            checksum: stream.ReadByte(),
            oemId: stream.ReadBytes(6).AsMemory(),
            oemTableId: stream.ReadBytes(8).AsMemory(),
            oemRevision: stream.ReadUInt32(),
            aslCompilerId: stream.ReadBytes(4).AsMemory(),
            aslCompilerRevision: stream.ReadUInt32());
    }
}

