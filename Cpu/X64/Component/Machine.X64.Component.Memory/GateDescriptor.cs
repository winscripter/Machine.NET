using System.Runtime.CompilerServices;

namespace Machine.X64.Component;

/// <summary>
/// Represents a 64-bit gate descriptor.
/// </summary>
public unsafe struct GateDescriptor
{
    private fixed byte _data[16];

    /// <summary>
    /// Gets or sets the low part of the offset.
    /// </summary>
    public ulong OffsetLow
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return *(ulong*)ptr;
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                *(ulong*)ptr = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the segment selector.
    /// </summary>
    public ushort Selector
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return *(ushort*)(ptr + 8);
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                *(ushort*)(ptr + 8) = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the IST offset.
    /// </summary>
    public byte IST
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return *(byte*)(ptr + 10);
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                *(byte*)(ptr + 10) = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the gate type.
    /// </summary>
    public byte GateType
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return *(byte*)(ptr + 11);
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                *(byte*)(ptr + 11) = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the Descriptor Privilege Level (DPL).
    /// </summary>
    public byte DPL
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return *(byte*)(ptr + 12);
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                *(byte*)(ptr + 12) = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets the present bit.
    /// </summary>
    public bool Present
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return (*(byte*)(ptr + 13) & 0x80) != 0;
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                if (value)
                {
                    *(byte*)(ptr + 13) |= 0x80;
                }
                else
                {
                    *(byte*)(ptr + 13) &= 0x7F;
                }
            }
        }
    }

    /// <summary>
    /// Gets or sets the high part of the offset.
    /// </summary>
    public ulong OffsetHigh
    {
        get
        {
            fixed (byte* ptr = _data)
            {
                return *(ulong*)(ptr + 14);
            }
        }
        set
        {
            fixed (byte* ptr = _data)
            {
                *(ulong*)(ptr + 14) = value;
            }
        }
    }

    /// <summary>
    /// Converts <see cref="Descriptor"/> to <see cref="GateDescriptor"/>.
    /// </summary>
    /// <param name="descriptor"><see cref="Descriptor"/></param>
    /// <returns><see cref="GateDescriptor"/></returns>
    public static GateDescriptor FromBase(Descriptor descriptor)
    {
#if MAX_PERF
        unsafe
        {
            return *(InterruptDescriptor*)&descriptor;
        }
#else
        return Unsafe.As<Descriptor, GateDescriptor>(ref descriptor);
#endif
    }
}
