using System.Runtime.Intrinsics;

namespace Machine.Utility;

public static class VectorUtilities
{
    public static Vector128<float> Arrange(Vector128<float> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector128<float> newVec = Vector128.Create<float>(0F);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector128<float>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector128<uint> Arrange(Vector128<uint> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector128<uint> newVec = Vector128.Create<uint>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector128<uint>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector128<ulong> Arrange(Vector128<ulong> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector128<ulong> newVec = Vector128.Create<ulong>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector128<ulong>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector256<uint> Arrange(Vector256<uint> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector256<uint> newVec = Vector256.Create<uint>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector256<uint>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector256<ulong> Arrange(Vector256<ulong> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector256<ulong> newVec = Vector256.Create<ulong>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector256<ulong>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector512<uint> Arrange(Vector512<uint> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector512<uint> newVec = Vector512.Create<uint>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector512<uint>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector512<ulong> Arrange(Vector512<ulong> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector512<ulong> newVec = Vector512.Create<ulong>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector512<ulong>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector128<double> Arrange(Vector128<double> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector128<double> newVec = Vector128.Create<double>(0D);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector128<double>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector256<float> Arrange(Vector256<float> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector256<float> newVec = Vector256.Create<float>(0F);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector256<float>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector256<double> Arrange(Vector256<double> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector256<double> newVec = Vector256.Create<double>(0D);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector256<double>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector512<float> Arrange(Vector512<float> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector512<float> newVec = Vector512.Create<float>(0F);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector512<float>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector512<double> Arrange(Vector512<double> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector512<double> newVec = Vector512.Create<double>(0D);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector512<double>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector128<int> Arrange(Vector128<int> vector)
    {
        // Examples:
        // 1. [1, 0, 3, 0] -> [1, 3, 0, 0]
        // 2. [0, 5, 8, 0] -> [5, 8, 0, 0]
        // 3. [0, 0, 7, 0] -> [7, 0, 0, 0]
        // 4. [5, 0, 8, 3] -> [5, 8, 3, 0]
        Vector128<int> newVec = Vector128.Create<int>(0);
        int lastValidIndex = 0;
        for (int i = 0; i < Vector128<int>.Count; i++)
        {
            if (vector[i] != 0F)
            {
                newVec = newVec.WithElement(lastValidIndex++, vector[i]);
            }
        }
        return newVec;
    }

    public static Vector64<Half> AsHalf<T>(this Vector64<T> vector) => vector.As<T, Half>();
    public static Vector128<Half> AsHalf<T>(this Vector128<T> vector) => vector.As<T, Half>();
    public static Vector256<Half> AsHalf<T>(this Vector256<T> vector) => vector.As<T, Half>();
    public static Vector512<Half> AsHalf<T>(this Vector512<T> vector) => vector.As<T, Half>();

    public static Vector128<T> K1z<T>(this Vector128<T> vector, T z, ulong k1)
    {
        for (int i = 0; i < Vector128<T>.Count; i++)
        {
            if (BitUtilities.IsBitSet(k1, i))
            {
                vector = vector.WithElement(i, z);
            }
        }
        return vector;
    }

    public static Vector128<Half> K1z(this Vector128<Half> vector, int z, ulong k1)
    {
        return vector.K1z((Half)z, k1);
    }

    public static Vector256<Half> K1z(this Vector256<Half> vector, int z, ulong k1)
    {
        return vector.K1z((Half)z, k1);
    }

    public static Vector512<Half> K1z(this Vector512<Half> vector, int z, ulong k1)
    {
        return vector.K1z((Half)z, k1);
    }

    public static Vector256<T> K1z<T>(this Vector256<T> vector, T z, ulong k1)
    {
        for (int i = 0; i < Vector256<T>.Count; i++)
        {
            if (BitUtilities.IsBitSet(k1, i))
            {
                vector = vector.WithElement(i, z);
            }
        }
        return vector;
    }

    public static Vector512<T> K1z<T>(this Vector512<T> vector, T z, ulong k1)
    {
        for (int i = 0; i < Vector512<T>.Count; i++)
        {
            if (BitUtilities.IsBitSet(k1, i))
            {
                vector = vector.WithElement(i, z);
            }
        }
        return vector;
    }

    public static Vector128<byte> InverseAll(this Vector128<byte> vector)
    {
        Vector128<byte> result = Vector128<byte>.Zero;
        for (int i = 0; i < Vector128<byte>.Count; i++)
            result = result.WithElement(i, (byte)~vector[i]);
        return result;
    }

    public static Vector256<byte> InverseAll(this Vector256<byte> vector)
    {
        Vector256<byte> result = Vector256<byte>.Zero;
        for (int i = 0; i < Vector256<byte>.Count; i++)
            result = result.WithElement(i, (byte)~vector[i]);
        return result;
    }

    public static Vector512<byte> InverseAll(this Vector512<byte> vector)
    {
        Vector512<byte> result = Vector512<byte>.Zero;
        for (int i = 0; i < Vector512<byte>.Count; i++)
            result = result.WithElement(i, (byte)~vector[i]);
        return result;
    }

    public static Vector128<float> Sqrt(this Vector128<float> vector)
    {
        Vector128<float> result = Vector128<float>.Zero;
        for (int i = 0; i < Vector128<float>.Count; i++)
            result = result.WithElement(i, MathF.Sqrt(vector[i]));
        return result;
    }

    public static Vector256<float> Sqrt(this Vector256<float> vector)
    {
        Vector256<float> result = Vector256<float>.Zero;
        for (int i = 0; i < Vector256<float>.Count; i++)
            result = result.WithElement(i, MathF.Sqrt(vector[i]));
        return result;
    }

    public static Vector512<float> Sqrt(this Vector512<float> vector)
    {
        Vector512<float> result = Vector512<float>.Zero;
        for (int i = 0; i < Vector512<float>.Count; i++)
            result = result.WithElement(i, MathF.Sqrt(vector[i]));
        return result;
    }

    public static Vector128<double> Sqrt(this Vector128<double> vector)
    {
        Vector128<double> result = Vector128<double>.Zero;
        for (int i = 0; i < Vector128<double>.Count; i++)
            result = result.WithElement(i, Math.Sqrt(vector[i]));
        return result;
    }

    public static Vector256<double> Sqrt(this Vector256<double> vector)
    {
        Vector256<double> result = Vector256<double>.Zero;
        for (int i = 0; i < Vector256<double>.Count; i++)
            result = result.WithElement(i, Math.Sqrt(vector[i]));
        return result;
    }

    public static Vector512<double> Sqrt(this Vector512<double> vector)
    {
        Vector512<double> result = Vector512<double>.Zero;
        for (int i = 0; i < Vector512<double>.Count; i++)
            result = result.WithElement(i, Math.Sqrt(vector[i]));
        return result;
    }
}
