using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Sse;

/// <summary>
/// Calculates minimum/maximum values of two vectors.
/// </summary>
public static class VectorMinMax
{
    /// <summary>
    /// Returns minimum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [4.0, 3.0, 6.0, 5.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with minimal values from both vectors.</returns>
    public static Vector128<float> OfMin(Vector128<float> vx, Vector128<float> vy)
    {
        var result = Vector128<float>.Zero;
        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            if (vx[i] < vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns maximum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [6.0, 9.0, 7.0, 8.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with max values from both vectors.</returns>
    public static Vector128<float> OfMax(Vector128<float> vx, Vector128<float> vy)
    {
        var result = Vector128<float>.Zero;
        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            if (vx[i] > vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns minimum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [4.0, 3.0, 6.0, 5.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with minimal values from both vectors.</returns>
    public static Vector128<double> OfMin(Vector128<double> vx, Vector128<double> vy)
    {
        var result = Vector128<double>.Zero;
        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            if (vx[i] < vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns maximum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [6.0, 9.0, 7.0, 8.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with max values from both vectors.</returns>
    public static Vector128<double> OfMax(Vector128<double> vx, Vector128<double> vy)
    {
        var result = Vector128<double>.Zero;
        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            if (vx[i] > vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    #region 256 bit representations
    /// <summary>
    /// Returns minimum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [4.0, 3.0, 6.0, 5.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with minimal values from both vectors.</returns>
    public static Vector256<float> OfMin(Vector256<float> vx, Vector256<float> vy)
    {
        var result = Vector256<float>.Zero;
        for (int i = 0; i < Vector256<float>.Count; i++)
        {
            if (vx[i] < vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns maximum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [6.0, 9.0, 7.0, 8.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with max values from both vectors.</returns>
    public static Vector256<float> OfMax(Vector256<float> vx, Vector256<float> vy)
    {
        var result = Vector256<float>.Zero;
        for (int i = 0; i < Vector256<float>.Count; i++)
        {
            if (vx[i] > vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns minimum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [4.0, 3.0, 6.0, 5.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with minimal values from both vectors.</returns>
    public static Vector256<double> OfMin(Vector256<double> vx, Vector256<double> vy)
    {
        var result = Vector256<double>.Zero;
        for (int i = 0; i < Vector256<double>.Count; i++)
        {
            if (vx[i] < vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns maximum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [6.0, 9.0, 7.0, 8.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with max values from both vectors.</returns>
    public static Vector256<double> OfMax(Vector256<double> vx, Vector256<double> vy)
    {
        var result = Vector256<double>.Zero;
        for (int i = 0; i < Vector256<double>.Count; i++)
        {
            if (vx[i] > vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }
    #endregion

    #region 512 bit representations
    /// <summary>
    /// Returns minimum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [4.0, 3.0, 6.0, 5.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with minimal values from both vectors.</returns>
    public static Vector512<float> OfMin(Vector512<float> vx, Vector512<float> vy)
    {
        var result = Vector512<float>.Zero;
        for (int i = 0; i < Vector512<float>.Count; i++)
        {
            if (vx[i] < vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns maximum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [6.0, 9.0, 7.0, 8.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with max values from both vectors.</returns>
    public static Vector512<float> OfMax(Vector512<float> vx, Vector512<float> vy)
    {
        var result = Vector512<float>.Zero;
        for (int i = 0; i < Vector512<float>.Count; i++)
        {
            if (vx[i] > vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns minimum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [4.0, 3.0, 6.0, 5.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with minimal values from both vectors.</returns>
    public static Vector512<double> OfMin(Vector512<double> vx, Vector512<double> vy)
    {
        var result = Vector512<double>.Zero;
        for (int i = 0; i < Vector512<double>.Count; i++)
        {
            if (vx[i] < vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }

    /// <summary>
    /// Returns maximum values from two vectors.
    /// <example>
    /// <para>Example:</para>
    /// <para>XMM1 = [6.0, 3.0, 7.0, 8.0]</para>
    /// <para>XMM2 = [4.0, 9.0, 6.0, 5.0]</para>
    /// <para>Result:</para>
    /// <para>XMM1 = [6.0, 9.0, 7.0, 8.0]</para>
    /// </example>
    /// </summary>
    /// <param name="vx">First vector.</param>
    /// <param name="vy">Second vector.</param>
    /// <returns>A new vector with max values from both vectors.</returns>
    public static Vector512<double> OfMax(Vector512<double> vx, Vector512<double> vy)
    {
        var result = Vector512<double>.Zero;
        for (int i = 0; i < Vector512<double>.Count; i++)
        {
            if (vx[i] > vy[i])
            {
                result = result.WithElement(i, vx[i]);
            }
            else // Also applies if two values are equal
            {
                result = result.WithElement(i, vy[i]);
            }
        }
        return result;
    }
    #endregion
}
