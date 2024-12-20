using System.Runtime.CompilerServices;

namespace Machine.X64.Component;

/// <summary>
/// Represents an interrupt descriptor.
/// </summary>
public struct InterruptDescriptor
{
    private ulong _data;

    /// <summary>
    /// The lower 16 bits of the ISR's address.
    /// </summary>
    public readonly ushort OffsetLow
    {
        get
        {
            return (ushort)(_data & 0xFFFF);
        }
    }

    /// <summary>
    /// A 16-bit field that specifies the segment selector for the code
    /// segment containing the ISR.
    /// </summary>
    public readonly ushort Selector
    {
        get
        {
            return (ushort)((_data >> 16) & 0xFFFF);
        }
    }

    /// <summary>
    /// A 5-bit field that is reserved and must be set to zero.
    /// </summary>
    public readonly byte Reserved
    {
        get
        {
            return (byte)((_data >> 32) & 0x1F);
        }
    }

    /// <summary>
    /// A 5-bit field that specifies the type of gate (e.g.,
    /// interrupt gate, trap gate, task gate).
    /// </summary>
    public readonly byte Type
    {
        get
        {
            return (byte)((_data >> 40) & 0x1F);
        }
    }

    /// <summary>
    /// A 2-bit field that specifies the privilege level required to access the descriptor.
    /// </summary>
    public readonly byte DescriptorPrivilegeLevel
    {
        get
        {
            return (byte)((_data >> 45) & 0x3);
        }
    }

    /// <summary>
    /// A 1-bit field that indicates whether the descriptor is present in memory.
    /// </summary>
    public readonly bool Present
    {
        get
        {
            return ((_data >> 47) & 0x1) == 1;
        }
    }

    /// <summary>
    /// The upper 16 bits of the ISR's address.
    /// </summary>
    public readonly ushort OffsetHigh
    {
        get
        {
            return (ushort)((_data >> 48) & 0xFFFF);
        }
    }

    /// <summary>
    /// Converts <see cref="Descriptor"/> to <see cref="InterruptDescriptor"/>.
    /// </summary>
    /// <param name="descriptor"><see cref="Descriptor"/></param>
    /// <returns><see cref="InterruptDescriptor"/></returns>
    public static InterruptDescriptor FromBase(Descriptor descriptor)
    {
#if MAX_PERF
        unsafe
        {
            return *(InterruptDescriptor*)&descriptor;
        }
#else
        return Unsafe.As<Descriptor, InterruptDescriptor>(ref descriptor);
#endif
    }
}
