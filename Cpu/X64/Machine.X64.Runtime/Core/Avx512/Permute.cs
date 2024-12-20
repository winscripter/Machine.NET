using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Avx512;

/// <summary>
/// Controls permutation of two vectors.
/// </summary>
public static class Permute
{
    /// <summary>
    /// Embed two vectors, like {X, Y, X, Y, X, Y, ...}. This is often used by the VPERMT2X instructions,
    /// where X can be B, W, D, or Q.
    /// </summary>
    /// <typeparam name="T">Types of two vectors.</typeparam>
    /// <param name="x">First vector.</param>
    /// <param name="y">Another vector.</param>
    /// <param name="defaultValues">Default values when creating the output vector. Can be anything; this won't impact anything.</param>
    /// <returns>A new vector with switching values.</returns>
    public static Vector128<T> Embed<T>(Vector128<T> x, Vector128<T> y, T defaultValues) where T : unmanaged
    {
        Vector128<T> result = Vector128.Create(defaultValues);
        for (int i = 0; i < Vector128<T>.Count; i++)
        {
            result = result.WithElement(i, (i % 2) == 0 ? x[i] : y[i]);
        }
        return result;
    }

    /// <summary>
    /// Embed two vectors, like {X, Y, X, Y, X, Y, ...}. This is often used by the VPERMT2X instructions,
    /// where X can be B, W, D, or Q.
    /// </summary>
    /// <typeparam name="T">Types of two vectors.</typeparam>
    /// <param name="x">First vector.</param>
    /// <param name="y">Another vector.</param>
    /// <param name="defaultValues">Default values when creating the output vector. Can be anything; this won't impact anything.</param>
    /// <returns>A new vector with switching values.</returns>
    public static Vector256<T> Embed<T>(Vector256<T> x, Vector256<T> y, T defaultValues) where T : unmanaged
    {
        Vector256<T> result = Vector256.Create(defaultValues);
        for (int i = 0; i < Vector256<T>.Count; i++)
        {
            result = result.WithElement(i, (i % 2) == 0 ? x[i] : y[i]);
        }
        return result;
    }

    /// <summary>
    /// Embed two vectors, like {X, Y, X, Y, X, Y, ...}. This is often used by the VPERMT2X instructions,
    /// where X can be B, W, D, or Q.
    /// </summary>
    /// <typeparam name="T">Types of two vectors.</typeparam>
    /// <param name="x">First vector.</param>
    /// <param name="y">Another vector.</param>
    /// <param name="defaultValues">Default values when creating the output vector. Can be anything; this won't impact anything.</param>
    /// <returns>A new vector with switching values.</returns>
    public static Vector512<T> Embed<T>(Vector512<T> x, Vector512<T> y, T defaultValues) where T : unmanaged
    {
        Vector512<T> result = Vector512.Create(defaultValues);
        for (int i = 0; i < Vector512<T>.Count; i++)
        {
            result = result.WithElement(i, (i % 2) == 0 ? x[i] : y[i]);
        }
        return result;
    }
}
