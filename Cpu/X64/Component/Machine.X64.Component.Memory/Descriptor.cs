using System.Runtime.CompilerServices;

namespace Machine.X64.Component;

/// <summary>
/// Represents a memory descriptor.
/// </summary>
public struct Descriptor
{
    private ushort offset1;
    private ushort selector;
    private byte ist;
    private byte typeAttributes;
    private ushort offset2;
    private uint offset3;
    private uint zero;

    /// <summary>
    /// Performs bit conversion from <see cref="ulong"/> to <see cref="Descriptor"/>.
    /// </summary>
    /// <param name="value">Input <see cref="ulong"/>.</param>
    /// <returns>Converted <see cref="Descriptor"/>.</returns>
    public static Descriptor FromUInt64(ulong value)
    {
#if MAX_PERF
        unsafe
        {
            return *(Descriptor*)&value;
        }
#else
        return Unsafe.As<ulong, Descriptor>(ref value);
#endif
    }
}
