using System.Runtime.CompilerServices;

namespace Machine.X64.Component;

/// <summary>
/// The segment descriptor.
/// </summary>
public struct SegmentDescriptor
{
    private ulong _data;

    /// <summary>
    /// This is a 32-bit field that specifies the starting address of the segment in
    /// the linear address space.
    /// </summary>
    public readonly uint BaseAddress
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            uint baseLow = (uint)(_data & 0xFFFF);
            uint baseMid = (uint)((_data >> 16) & 0xFF);
            uint baseHigh = (uint)((_data >> 56) & 0xFF);
            return baseLow | (baseMid << 16) | (baseHigh << 24);
        }
    }

    /// <summary>
    /// This 20-bit field specifies the size of the segment. It can be scaled by a
    /// granularity bit to represent sizes up to 4 GB.
    /// </summary>
    public readonly uint SegmentLimit
    {
        get
        {
            uint limitLow = (uint)(_data & 0xFFFF);
            uint limitHigh = (uint)((_data >> 48) & 0xF);
            return limitLow | (limitHigh << 16);
        }
    }

    /// <summary>
    /// This field indicates the segment type and its access rights. It includes bits for:
    /// <list type="number">
    ///   <item>
    ///     <para><b>Code/Data Segment</b>: Differentiates between code and data segments.</para>  
    ///   </item>
    ///   <item>
    ///     <para><b>Executable</b>: Indicates if the segment contains executable code.</para>
    ///   </item>
    ///   <item>
    ///     <para><b>Expand-Down</b>: For data segments, indicates if the segment grows downwards.</para>
    ///   </item>
    ///   <item>
    ///     <para><b>Writeable/Readable</b>: Indicates if the data segment is writable or if the code segment is readable.</para>
    ///   </item>
    ///   <item>
    ///     <para><b>Accessed</b>: Indicates if the segment has been accessed.</para>
    ///   </item>
    /// </list>
    /// </summary>
    public readonly byte Type
    {
        get
        {
            return (byte)((_data >> 40) & 0x1F);
        }
    }

    /// <summary>
    /// This 2-bit field specifies the privilege level of the segment,
    /// ranging from 0 (highest privilege) to 3 (lowest privilege).
    /// </summary>
    public readonly byte DescriptorPrivilegeLevel
    {
        get
        {
            return (byte)((_data >> 45) & 0x3);
        }
    }

    /// <summary>
    /// This bit indicates whether the segment is present in memory.
    /// If it is not set, a segment-not-present exception is generated when
    /// the segment is accessed.
    /// </summary>
    public readonly bool SegmentPresent
    {
        get
        {
            return ((_data >> 47) & 0x1) == 1;
        }
    }

    /// <summary>
    /// This bit determines the scaling of the segment limit.
    /// If set, the limit is multiplied by 4 KB.
    /// </summary>
    public readonly bool Granularity
    {
        get
        {
            return ((_data >> 55) & 0x1) == 1;
        }
    }

    /// <summary>
    /// This bit specifies the default size of the operands for instructions in the segment.
    /// If set, the default size is 32 bits; otherwise, it is 16 bits.
    /// </summary>
    public readonly bool DefaultOperationSize
    {
        get
        {
            return ((_data >> 54) & 0x1) == 1;
        }
    }

    /// <summary>
    /// Returns the base address, multiplied by 16 if <see cref="Granularity"/> bit is set to 1.
    /// </summary>
    /// <returns>A base address that matches with the granularity.</returns>
    public readonly uint GetAddress()
    {
        return Granularity ? BaseAddress * 16 : BaseAddress;
    }

    /// <summary>
    /// Converts <see cref="Descriptor"/> to <see cref="SegmentDescriptor"/>.
    /// </summary>
    /// <param name="descriptor"><see cref="Descriptor"/></param>
    /// <returns><see cref="SegmentDescriptor"/></returns>
    public static SegmentDescriptor FromBase(Descriptor descriptor)
    {
#if MAX_PERF
        unsafe
        {
            return *(SegmentDescriptor*)&descriptor;
        }
#else
        return Unsafe.As<Descriptor, SegmentDescriptor>(ref descriptor);
#endif
    }
}
