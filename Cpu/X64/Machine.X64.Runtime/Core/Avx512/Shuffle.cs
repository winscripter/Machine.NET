using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Avx512;

/// <summary>
/// Simulates shuffling of vectors.
/// </summary>
public static class Shuffle
{
    /// <summary>
    /// Reads all shuffling modes, returning four <see cref="VectorShufflingMode"/> instances in
    /// a read-only span.
    /// </summary>
    /// <param name="controlMask">The control mask.</param>
    /// <returns>A read-only span.</returns>
    public static ReadOnlySpan<VectorShufflingMode> ReadShufflingModes(byte controlMask)
    {
        byte firstField = (byte)(controlMask & 0b00000011);
        byte secondField = (byte)((controlMask >> 2) & 0b00000011);
        byte thirdField = (byte)((controlMask >> 4) & 0b00000011);
        byte fourthField = (byte)((controlMask >> 6) & 0b00000011);

        return new ReadOnlySpan<VectorShufflingMode>(
            [
                (VectorShufflingMode)firstField,
                (VectorShufflingMode)secondField,
                (VectorShufflingMode)thirdField,
                (VectorShufflingMode)fourthField
            ]
        );
    }

    /// <summary>
    /// Shuffles two 256-bit vectors based on the control mask, returning a new 256-bit vector
    /// with values from two 256-bit vectors shuffled according to the control mask.
    /// </summary>
    /// <remarks>
    ///   The control mask is the <paramref name="modes"/> parameter. Use the
    ///   <see cref="ReadShufflingModes(byte)"/> method to convert the control
    ///   mask of type <see cref="byte"/> into <see cref="ReadOnlySpan{T}"/>
    ///   (where T is <see cref="VectorShufflingMode"/>) which is acceptable
    ///   by this method.
    /// </remarks>
    /// <param name="x">The first input 256-bit vector.</param>
    /// <param name="y">The second input 256-bit vector.</param>
    /// <param name="modes">The control mask.</param>
    /// <returns>
    ///   A new 256-bit vector with values from two input 256-bit vectors shuffled
    ///   based on the control mask.
    /// </returns>
    /// <exception cref="NotImplementedException">
    ///   Thrown when the <see cref="VectorShufflingMode"/> enumeration contains an
    ///   unrecognized value.
    /// </exception>
    public static Vector256<float> Vectors(Vector256<float> x, Vector256<float> y, ReadOnlySpan<VectorShufflingMode> modes)
    {
        return Vector256.Create<float>(
                Vector128.Create<float>(
                    modes[0] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetLower().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetLower().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetLower().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetLower().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[0]}'")
                    },
                    modes[1] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetUpper().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetUpper().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetUpper().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetUpper().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[1]}'")
                    }
                ),
                Vector128.Create<float>(
                    modes[2] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetLower().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetLower().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetLower().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetLower().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[2]}'")
                    },
                    modes[3] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetUpper().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetUpper().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetUpper().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetUpper().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[3]}'")
                    }
                )
            );
    }

    /// <summary>
    /// Shuffles two 512-bit vectors based on the control mask, returning a new 512-bit vector
    /// with values from two 512-bit vectors shuffled according to the control mask.
    /// </summary>
    /// <remarks>
    ///   The control mask is the <paramref name="modes"/> parameter. Use the
    ///   <see cref="ReadShufflingModes(byte)"/> method to convert the control
    ///   mask of type <see cref="byte"/> into <see cref="ReadOnlySpan{T}"/>
    ///   (where T is <see cref="VectorShufflingMode"/>) which is acceptable
    ///   by this method.
    /// </remarks>
    /// <param name="x">The first input 512-bit vector.</param>
    /// <param name="y">The second input 512-bit vector.</param>
    /// <param name="modes">The control mask.</param>
    /// <returns>
    ///   A new 512-bit vector with values from two input 512-bit vectors shuffled
    ///   based on the control mask.
    /// </returns>
    /// <exception cref="NotImplementedException">
    ///   Thrown when the <see cref="VectorShufflingMode"/> enumeration contains an
    ///   unrecognized value.
    /// </exception>
    public static Vector512<float> Vectors(Vector512<float> x, Vector512<float> y, ReadOnlySpan<VectorShufflingMode> modes)
    {
        return Vector512.Create<float>(
                Vector256.Create<float>(
                    modes[0] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetLower().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetLower().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetLower().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetLower().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[0]}'")
                    },
                    modes[1] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetUpper().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetUpper().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetUpper().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetUpper().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[1]}'")
                    }
                ),
                Vector256.Create<float>(
                    modes[2] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetLower().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetLower().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetLower().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetLower().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[2]}'")
                    },
                    modes[3] switch
                    {
                        VectorShufflingMode.LowFirst => x.GetUpper().GetLower(),
                        VectorShufflingMode.HighFirst => x.GetUpper().GetUpper(),
                        VectorShufflingMode.LowSecond => y.GetUpper().GetLower(),
                        VectorShufflingMode.HighSecond => y.GetUpper().GetUpper(),
                        _ => throw new NotImplementedException($"Invalid shuffling mode '{modes[3]}'")
                    }
                )
            );
    }
}
