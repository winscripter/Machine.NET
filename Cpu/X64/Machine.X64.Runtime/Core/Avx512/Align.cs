using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Avx512;

/// <summary>
/// Aligns vectors.
/// </summary>
public static class Align
{
    /// <summary>
    /// Aligns two 128-bit vectors of any unmanaged type.
    /// </summary>
    /// <typeparam name="T">Types of vectors.</typeparam>
    /// <param name="x">First vector.</param>
    /// <param name="y">Second vector.</param>
    /// <param name="imm8">Shift count.</param>
    /// <returns>A new, aligned 128-bit vector of type <typeparamref name="T"/>.</returns>
    public static Vector128<T> Vectors<T>(Vector128<T> x, Vector128<T> y, byte imm8) where T : unmanaged
    {
        int count = Vector128<T>.Count;
        var result = new Span<T>(new T[count]);
        int shift = imm8 & (count - 1);
        for (int i = 0; i < count; i++)
        {
            if (i + shift < count)
            {
                result[i] = x.GetElement(i + shift);
            }
            else
            {
                result[i] = y.GetElement(i + shift - count);
            }
        }
        return Vector128.Create<T>(result);
    }

    /// <summary>
    /// Aligns two 256-bit vectors of any unmanaged type.
    /// </summary>
    /// <typeparam name="T">Types of vectors.</typeparam>
    /// <param name="x">First vector.</param>
    /// <param name="y">Second vector.</param>
    /// <param name="imm8">Shift count.</param>
    /// <returns>A new, aligned 256-bit vector of type <typeparamref name="T"/>.</returns>
    public static Vector256<T> Vectors<T>(Vector256<T> x, Vector256<T> y, byte imm8) where T : unmanaged
    {
        int count = Vector256<T>.Count;
        var result = new Span<T>(new T[count]);
        int shift = imm8 & (count - 1);
        for (int i = 0; i < count; i++)
        {
            if (i + shift < count)
            {
                result[i] = x.GetElement(i + shift);
            }
            else
            {
                result[i] = y.GetElement(i + shift - count);
            }
        }
        return Vector256.Create<T>(result);
    }

    /// <summary>
    /// Aligns two 128-bit vectors of any unmanaged type.
    /// </summary>
    /// <typeparam name="T">Types of vectors.</typeparam>
    /// <param name="x">First vector.</param>
    /// <param name="y">Second vector.</param>
    /// <param name="imm8">Shift count.</param>
    /// <returns>A new, aligned 128-bit vector of type <typeparamref name="T"/>.</returns>
    public static Vector512<T> Vectors<T>(Vector512<T> x, Vector512<T> y, byte imm8) where T : unmanaged
    {
        int count = Vector512<T>.Count;
        var result = new Span<T>(new T[count]);
        int shift = imm8 & (count - 1);
        for (int i = 0; i < count; i++)
        {
            if (i + shift < count)
            {
                result[i] = x.GetElement(i + shift);
            }
            else
            {
                result[i] = y.GetElement(i + shift - count);
            }
        }
        return Vector512.Create<T>(result);
    }
}
