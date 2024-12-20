using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Sse;

/// <summary>
/// Allows unpacking and interleaving vectors.
/// </summary>
public static class VectorUnpackInterleave
{
    /// <summary>
    /// Unpack &amp; Interleave high order bytes of vectors.
    /// </summary>
    /// <param name="x">Vector 1</param>
    /// <param name="y">Vector 2</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<byte> UnpackInterleaveHighOrder(Vector128<byte> x, Vector128<byte> y)
    {
        Vector128<byte> highOrderX = Vector128<byte>.Zero;
        Vector128<byte> highOrderY = Vector128<byte>.Zero;

        for (int i = 0; i < 8; i++)
        {
            highOrderX = highOrderX.WithElement(i, x.GetElement(i + 8));
            highOrderY = highOrderY.WithElement(i, y.GetElement(i + 8));
        }

        Vector128<byte> result = Vector128<byte>.Zero;
        for (int i = 0; i < 8; i++)
        {
            result = result.WithElement(2 * i, highOrderX.GetElement(i));
            result = result.WithElement(2 * i + 1, highOrderY.GetElement(i));
        }

        return result;
    }

    /// <summary>
    /// Unpack &amp; Interleave high order bytes of vectors.
    /// </summary>
    /// <param name="x">Vector 1</param>
    /// <param name="y">Vector 2</param>
    /// <returns><see cref="Vector64{T}"/></returns>
    public static Vector64<byte> UnpackInterleaveHighOrder(Vector64<byte> x, Vector64<byte> y)
    {
        Vector64<byte> highOrderX = Vector64<byte>.Zero;
        Vector64<byte> highOrderY = Vector64<byte>.Zero;

        for (int i = 0; i < 4; i++)
        {
            highOrderX = highOrderX.WithElement(i, x.GetElement(i + 8));
            highOrderY = highOrderY.WithElement(i, y.GetElement(i + 8));
        }

        Vector64<byte> result = Vector64<byte>.Zero;
        for (int i = 0; i < 4; i++)
        {
            result = result.WithElement(2 * i, highOrderX.GetElement(i));
            result = result.WithElement(2 * i + 1, highOrderY.GetElement(i));
        }

        return result;
    }

    /// <summary>
    /// Unpack &amp; Interleave high order words of vectors.
    /// </summary>
    /// <param name="x">Vector 1</param>
    /// <param name="y">Vector 2</param>
    /// <returns><see cref="Vector64{T}"/></returns>
    public static Vector64<ushort> UnpackInterleaveHighOrder(Vector64<ushort> x, Vector64<ushort> y)
    {
        Vector64<ushort> highOrderX = Vector64<ushort>.Zero;
        Vector64<ushort> highOrderY = Vector64<ushort>.Zero;

        for (int i = 0; i < 2; i++)
        {
            highOrderX = highOrderX.WithElement(i, x.GetElement(i + 8));
            highOrderY = highOrderY.WithElement(i, y.GetElement(i + 8));
        }

        Vector64<ushort> result = Vector64<ushort>.Zero;
        for (int i = 0; i < 2; i++)
        {
            result = result.WithElement(2 * i, highOrderX.GetElement(i));
            result = result.WithElement(2 * i + 1, highOrderY.GetElement(i));
        }

        return result;
    }

    /// <summary>
    /// Unpack &amp; Interleave high order doublewords of vectors.
    /// </summary>
    /// <param name="x">Vector 1</param>
    /// <param name="y">Vector 2</param>
    /// <returns><see cref="Vector64{T}"/></returns>
    public static Vector64<uint> UnpackInterleaveHighOrder(Vector64<uint> x, Vector64<uint> y)
    {
        Vector64<uint> highOrderX = Vector64<uint>.Zero;
        Vector64<uint> highOrderY = Vector64<uint>.Zero;

        for (int i = 0; i < 1; i++)
        {
            highOrderX = highOrderX.WithElement(i, x.GetElement(i + 8));
            highOrderY = highOrderY.WithElement(i, y.GetElement(i + 8));
        }

        Vector64<uint> result = Vector64<uint>.Zero;
        for (int i = 0; i < 1; i++)
        {
            result = result.WithElement(2 * i, highOrderX.GetElement(i));
            result = result.WithElement(2 * i + 1, highOrderY.GetElement(i));
        }

        return result;
    }

    /// <summary>
    /// Unpack &amp; Interleave high order bytes of words.
    /// </summary>
    /// <param name="x">Vector 1</param>
    /// <param name="y">Vector 2</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<ushort> UnpackInterleaveHighOrder(Vector128<ushort> x, Vector128<ushort> y)
    {
        Vector128<ushort> highOrderX = Vector128<ushort>.Zero;
        Vector128<ushort> highOrderY = Vector128<ushort>.Zero;

        for (int i = 0; i < 4; i++)
        {
            highOrderX = highOrderX.WithElement(i, x.GetElement(i + 4));
            highOrderY = highOrderY.WithElement(i, y.GetElement(i + 4));
        }

        Vector128<ushort> result = Vector128<ushort>.Zero;
        for (int i = 0; i < 4; i++)
        {
            result = result.WithElement(2 * i, highOrderX.GetElement(i));
            result = result.WithElement(2 * i + 1, highOrderY.GetElement(i));
        }

        return result;
    }

    /// <summary>
    /// Unpack &amp; Interleave high order bytes of doublewords.
    /// </summary>
    /// <param name="x">Vector 1</param>
    /// <param name="y">Vector 2</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<uint> UnpackInterleaveHighOrder(Vector128<uint> x, Vector128<uint> y)
    {
        Vector128<uint> highOrderX = Vector128<uint>.Zero;
        Vector128<uint> highOrderY = Vector128<uint>.Zero;

        for (int i = 0; i < 2; i++)
        {
            highOrderX = highOrderX.WithElement(i, x.GetElement(i + 8));
            highOrderY = highOrderY.WithElement(i, y.GetElement(i + 8));
        }

        Vector128<uint> result = Vector128<uint>.Zero;
        for (int i = 0; i < 2; i++)
        {
            result = result.WithElement(2 * i, highOrderX.GetElement(i));
            result = result.WithElement(2 * i + 1, highOrderY.GetElement(i));
        }

        return result;
    }
}
