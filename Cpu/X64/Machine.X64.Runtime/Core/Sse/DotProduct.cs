using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Sse;

/// <summary>
/// Calculates dot products of vectors. This is akin to the DPPS and DPPD SSE 4.1 instructions.
/// </summary>
public static class DotProduct
{
    /// <summary>
    /// Calculates the dot product for 128-bit single precision floating point integers.
    /// </summary>
    /// <param name="v1">Destination.</param>
    /// <param name="v2">Source.</param>
    /// <param name="imm8">Imm8</param>
    /// <returns>Result of the dot product.</returns>
    public static Vector128<float> CalculateSingle128(Vector128<float> v1, Vector128<float> v2, byte imm8)
    {
        Vector128<float> result = Vector128<float>.Zero;
        float dotProduct = 0.0F;

        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            if ((imm8 & (1 << (i + Vector128<float>.Count))) != 0)
            {
                dotProduct += v1[i] * v2[i];
            }
        }

        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            if ((imm8 & (1 << i)) != 0)
            {
                result = result.WithElement(i, dotProduct);
            }
        }

        return result;
    }

    /// <summary>
    /// Calculates the dot product for 128-bit double precision floating point integers.
    /// </summary>
    /// <param name="v1">Destination.</param>
    /// <param name="v2">Source.</param>
    /// <param name="imm8">Imm8</param>
    /// <returns>Result of the dot product.</returns>
    public static Vector128<double> CalculateDouble128(Vector128<double> v1, Vector128<double> v2, byte imm8)
    {
        Vector128<double> result = Vector128<double>.Zero;
        double dotProduct = 0.0D;

        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            if ((imm8 & (1 << (i + Vector128<double>.Count))) != 0)
            {
                dotProduct += v1[i] * v2[i];
            }
        }

        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            if ((imm8 & (1 << i)) != 0)
            {
                result = result.WithElement(i, dotProduct);
            }
        }

        return result;
    }
}
