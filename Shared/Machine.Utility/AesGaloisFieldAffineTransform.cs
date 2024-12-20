namespace Machine.Utility;

/// <summary>
/// Galois Field Affine Transform, dedicated for AES operations.
/// </summary>
public static class AesGaloisFieldAffineTransform
{
    private static readonly byte[] A =
    [
        0b11110001, // 0xF1
        0b01111000, // 0x78
        0b10111100, // 0xBC
        0b01011110, // 0x5E
        0b00101111, // 0x2F
        0b10010111, // 0x97
        0b11001011, // 0xCB
        0b11100101  // 0xE5
    ];

    private static readonly byte[] AInverse =
    [
        0b11100101, // 0xE5
        0b11001011, // 0xCB
        0b10010111, // 0x97
        0b00101111, // 0x2F
        0b01011110, // 0x5E
        0b10111100, // 0xBC
        0b01111000, // 0x78
        0b11110001  // 0xF1
    ];

    private const byte B = 0x63;
    private const byte BInverse = 0x05;

    /// <summary>
    /// Performs Galois Field Affine Transform for this byte.
    /// </summary>
    /// <param name="x">Byte</param>
    /// <returns>Byte</returns>
    public static byte AffineTransform(byte x)
    {
        byte result = 0;
        for (int i = 0; i < 8; i++)
        {
            byte bit = 0;
            for (int j = 0; j < 8; j++)
            {
                if ((x & (1 << j)) != 0 && (A[i] & (1 << j)) != 0)
                    bit ^= 1;
            }

            result |= (byte)(bit << i);
        }

        return (byte)(result ^ B);
    }

    /// <summary>
    /// Performs Inverse Galois Field Affine Transform for this byte.
    /// </summary>
    /// <param name="x">Byte</param>
    /// <returns>Byte</returns>
    public static byte InverseAffineTransform(byte y)
    {
        byte result = 0;
        for (int i = 0; i < 8; i++)
        {
            byte bit = 0;
            for (int j = 0; j < 8; j++)
            {
                if ((y & (1 << j)) != 0 && (AInverse[i] & (1 << j)) != 0)
                    bit ^= 1;
            }

            result |= (byte)(bit << i);
        }

        return (byte)(result ^ BInverse);
    }
}
