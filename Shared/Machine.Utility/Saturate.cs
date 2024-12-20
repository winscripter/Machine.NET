using System.Runtime.CompilerServices;

namespace Machine.Utility;

/// <summary>
/// Allows saturation of integers.
/// </summary>
public static class Saturate
{
    /// <summary>
    /// Performs saturation of a signed int32, returning signed int16.
    /// </summary>
    /// <param name="value">Input signed int32.</param>
    /// <returns>Signed int16, saturated from <paramref name="value"/>.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short UInt16Signed(int value)
    {
        if (value > short.MaxValue)
            return short.MaxValue;
        
        if (value < short.MinValue)
            return short.MinValue;
        
        return (short)value;
    }
}
