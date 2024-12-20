using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Sse;

/// <summary>
/// Blends vectors.
/// </summary>
public static class Blend
{
    /// <summary>
    /// Blends two 128-bit single precision floating point vectors based on the immediate as mask.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <param name="mask">The mask for blending.</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<float> VectorsSingle128(Vector128<float> a, Vector128<float> b, byte mask)
    {
        Vector128<float> result = Vector128<float>.Zero;

        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            if ((mask & (1 << i)) != 0)
            {
                result = result.WithElement(i, b[i]);
            }
            else
            {
                result = result.WithElement(i, a[i]);
            }
        }

        return result;
    }

    /// <summary>
    /// Blends two 128-bit single precision floating point vectors based on the XMM0 (Vector128 of type single-precision floating-point integer) as mask.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <param name="mask">The mask for blending.</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<float> VectorsSingle128(Vector128<float> a, Vector128<float> b, Vector128<float> mask)
    {
        Vector128<float> result = Vector128<float>.Zero;

        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            result = result.WithElement(i, (mask[i] != 0) ? b[i] : a[i]);
        }

        return result;
    }

    /// <summary>
    /// Blends two 128-bit double precision floating point vectors based on the immediate as mask.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <param name="mask">The mask for blending.</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<double> VectorsDouble128(Vector128<double> a, Vector128<double> b, byte mask)
    {
        Vector128<double> result = Vector128<double>.Zero;

        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            if ((mask & (1 << i)) != 0)
            {
                result = result.WithElement(i, b[i]);
            }
            else
            {
                result = result.WithElement(i, a[i]);
            }
        }

        return result;
    }

    /// <summary>
    /// Blends two 128-bit double precision floating point vectors based on the XMM0 (Vector128 of type single-precision doubleing-point integer) as mask.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <param name="mask">The mask for blending.</param>
    /// <returns><see cref="Vector128{T}"/></returns>
    public static Vector128<double> VectorsDouble128(Vector128<double> a, Vector128<double> b, Vector128<double> mask)
    {
        Vector128<double> result = Vector128<double>.Zero;

        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            result = result.WithElement(i, (mask[i] != 0) ? b[i] : a[i]);
        }

        return result;
    }
}
