using System.Runtime.Intrinsics;

namespace Machine.Utility;

/// <summary>
/// Galois Field Transform.
/// </summary>
public static class GaloisFieldTransform
{
    private const ushort IrreduciblePoly = 0x11B;

    /// <summary>
    /// Performs Galois Field Multiply Bytes with the default irreducible polynomial.
    /// </summary>
    /// <param name="a">Left value</param>
    /// <param name="b">Right value</param>
    /// <returns>
    /// <paramref name="a"/> and <paramref name="b"/>, multiplied according to the Galois Field
    /// Multiplication algorithm with default irreducible polynomial, which is equal to 0x11B.
    /// </returns>
    public static byte Multiply(byte a, byte b)
    {
        byte result = 0;
        byte tempB = b;

        for (int i = 0; i < 8; i++)
        {
            if ((a & 1) != 0)
            {
                result ^= tempB;
            }

            a >>= 1;

            if ((tempB & 0x80) != 0)
            {
                tempB = (byte)((tempB << 1) ^ IrreduciblePoly);
            }
            else
            {
                tempB <<= 1;
            }
        }

        return result;
    }

    /// <summary>
    /// Performs Galois Field Multiply Bytes with the custom irreducible polynomial.
    /// </summary>
    /// <param name="a">Left value</param>
    /// <param name="b">Right value</param>
    /// <param name="irreduciblePolynomial">Irreducible polynomial</param>
    /// <returns>
    /// <paramref name="a"/> and <paramref name="b"/>, multiplied according to the Galois Field
    /// Multiplication algorithm with custom irreducible polynomial.
    /// </returns>
    public static byte Multiply(byte a, byte b, byte irreduciblePolynomial)
    {
        byte result = 0;
        byte tempB = b;

        for (int i = 0; i < 8; i++)
        {
            if ((a & 1) != 0)
            {
                result ^= tempB;
            }

            a >>= 1;

            if ((tempB & 0x80) != 0)
            {
                tempB = (byte)((tempB << 1) ^ irreduciblePolynomial);
            }
            else
            {
                tempB <<= 1;
            }
        }

        return result;
    }

    /// <summary>
    /// Performs Galois Field Multiply Bytes with the custom irreducible polynomial.
    /// </summary>
    /// <param name="a">Left value</param>
    /// <param name="b">Right value</param>
    /// <param name="irreduciblePolynomial">Irreducible polynomial</param>
    /// <returns>
    /// <paramref name="a"/> and <paramref name="b"/>, multiplied according to the Galois Field
    /// Multiplication algorithm with custom irreducible polynomial.
    /// </returns>
    public static byte Multiply(byte a, byte b, ushort irreduciblePolynomial)
    {
        byte result = 0;
        byte tempB = b;

        for (int i = 0; i < 8; i++)
        {
            if ((a & 1) != 0)
            {
                result ^= tempB;
            }

            a >>= 1;

            if ((tempB & 0x80) != 0)
            {
                tempB = (byte)((tempB << 1) ^ irreduciblePolynomial);
            }
            else
            {
                tempB <<= 1;
            }
        }

        return result;
    }

    /// <summary>
    /// Performs Galois Field Affine transformation.
    /// </summary>
    /// <param name="x">The data</param>
    /// <param name="matrix">The matrix</param>
    /// <param name="constant">The constant</param>
    public static byte AffineTransform(byte x, Vector128<byte> matrix, byte constant)
    {
        byte result = 0;

        for (int i = 0; i < 8; i++)
        {
            byte bit = 0;
            for (int j = 0; j < 8; j++)
            {
                if ((x & (1 << j)) != 0 && (matrix[i] & (1 << j)) != 0)
                {
                    bit ^= 1;
                }
            }

            result |= (byte)(bit << i);
        }

        return (byte)(result ^ constant);
    }

    /// <summary>
    /// Performs Galois Field Affine transformation.
    /// </summary>
    /// <param name="x">The data</param>
    /// <param name="matrix">The matrix</param>
    /// <param name="constant">The constant</param>
    public static byte AffineTransform(byte x, Vector256<byte> matrix, byte constant)
    {
        byte result = 0;

        for (int i = 0; i < 16; i++)
        {
            byte bit = 0;
            for (int j = 0; j < 16; j++)
            {
                if ((x & (1 << j)) != 0 && (matrix[i] & (1 << j)) != 0)
                {
                    bit ^= 1;
                }
            }

            result |= (byte)(bit << i);
        }

        return (byte)(result ^ constant);
    }

    /// <summary>
    /// Performs Galois Field Affine transformation.
    /// </summary>
    /// <param name="x">The data</param>
    /// <param name="matrix">The matrix</param>
    /// <param name="constant">The constant</param>
    public static byte AffineTransform(byte x, Vector512<byte> matrix, byte constant)
    {
        byte result = 0;

        for (int i = 0; i < 32; i++)
        {
            byte bit = 0;
            for (int j = 0; j < 32; j++)
            {
                if ((x & (1 << j)) != 0 && (matrix[i] & (1 << j)) != 0)
                {
                    bit ^= 1;
                }
            }

            result |= (byte)(bit << i);
        }

        return (byte)(result ^ constant);
    }
}
