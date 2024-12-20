// Rounding mode is specified by the lower 2 bits which corresponds.
// See MxcsrRoundingControl to view their mappings.
// 
// If the value is too large, such as 42, it wouldn't result in an error...
// it would just take the lower two bits, which are 10 which correspond to
// round up (towards positive infinity). I guess the meaning of life is
// positive infinity when 42 is the number of life and its lower 2 bits are 10
// which correspond to positive infinity.

using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime.Core.Sse;

/// <summary>
/// Rounds vectors with rounding mode.
/// </summary>
public static class Round
{
    public static Vector128<float> VectorsSingle128(Vector128<float> vector, byte imm8)
    {
        MxcsrRoundingControl roundingControl = (MxcsrRoundingControl)(byte)(imm8 & 0b00000011);
        return roundingControl switch
        {
            MxcsrRoundingControl.Even => Vector128.Create(
                MathF.Truncate(vector[0]),
                MathF.Truncate(vector[1]),
                MathF.Truncate(vector[2]),
                MathF.Truncate(vector[3])
            ),
            MxcsrRoundingControl.TowardNegativeInfinity => Vector128.Create(
                MathF.Round(vector[0]),
                MathF.Round(vector[1]),
                MathF.Round(vector[2]),
                MathF.Round(vector[3])
            ),
            MxcsrRoundingControl.TowardPositiveInfinity => Vector128.Create(
                MathF.Floor(vector[0]),
                MathF.Floor(vector[1]),
                MathF.Floor(vector[2]),
                MathF.Floor(vector[3])
            ),
            MxcsrRoundingControl.Truncate => Vector128.Create(
                MathF.Ceiling(vector[0]),
                MathF.Ceiling(vector[1]),
                MathF.Ceiling(vector[2]),
                MathF.Ceiling(vector[3])
            ),
            _ => vector
        };
    }

    public static Vector128<double> VectorsDouble128(Vector128<double> vector, byte imm8)
    {
        MxcsrRoundingControl roundingControl = (MxcsrRoundingControl)(byte)(imm8 & 0b00000011);
        return roundingControl switch
        {
            MxcsrRoundingControl.Even => Vector128.Create(
                Math.Truncate(vector[0]),
                Math.Truncate(vector[1])
            ),
            MxcsrRoundingControl.TowardNegativeInfinity => Vector128.Create(
                Math.Round(vector[0]),
                Math.Round(vector[1])
            ),
            MxcsrRoundingControl.TowardPositiveInfinity => Vector128.Create(
                Math.Floor(vector[0]),
                Math.Floor(vector[1])
            ),
            MxcsrRoundingControl.Truncate => Vector128.Create(
                Math.Ceiling(vector[0]),
                Math.Ceiling(vector[1])
            ),
            _ => vector
        };
    }

    public static Vector128<float> ScalarSingle128(Vector128<float> vector, byte imm8)
    {
        MxcsrRoundingControl roundingControl = (MxcsrRoundingControl)(byte)(imm8 & 0b00000011);
        switch (roundingControl)
        {
            case MxcsrRoundingControl.Even:
                vector = vector.WithElement(0, MathF.Truncate(vector.ToScalar()));
                break;

            case MxcsrRoundingControl.TowardNegativeInfinity:
                vector = vector.WithElement(0, MathF.Round(vector.ToScalar()));
                break;

            case MxcsrRoundingControl.TowardPositiveInfinity:
                vector = vector.WithElement(0, MathF.Floor(vector.ToScalar()));
                break;

            case MxcsrRoundingControl.Truncate:
                vector = vector.WithElement(0, MathF.Ceiling(vector.ToScalar()));
                break;
        }
        return vector;
    }

    public static Vector128<double> ScalarDouble128(Vector128<double> vector, byte imm8)
    {
        MxcsrRoundingControl roundingControl = (MxcsrRoundingControl)(byte)(imm8 & 0b00000011);
        switch (roundingControl)
        {
            case MxcsrRoundingControl.Even:
                vector = vector.WithElement(0, Math.Truncate(vector.ToScalar()));
                break;

            case MxcsrRoundingControl.TowardNegativeInfinity:
                vector = vector.WithElement(0, Math.Round(vector.ToScalar()));
                break;

            case MxcsrRoundingControl.TowardPositiveInfinity:
                vector = vector.WithElement(0, Math.Floor(vector.ToScalar()));
                break;

            case MxcsrRoundingControl.Truncate:
                vector = vector.WithElement(0, Math.Ceiling(vector.ToScalar()));
                break;
        }
        return vector;
    }

    public static float Single(float value, byte imm8)
    {
        MxcsrRoundingControl roundingControl = (MxcsrRoundingControl)(byte)(imm8 & 0b00000011);
        return roundingControl switch
        {
            MxcsrRoundingControl.Even => MathF.Truncate(value),
            MxcsrRoundingControl.TowardNegativeInfinity => MathF.Round(value),
            MxcsrRoundingControl.TowardPositiveInfinity => MathF.Floor(value),
            MxcsrRoundingControl.Truncate => MathF.Ceiling(value),
            _ => 0F
        };
    }

    public static double Double(double value, byte imm8)
    {
        MxcsrRoundingControl roundingControl = (MxcsrRoundingControl)(byte)(imm8 & 0b00000011);
        return roundingControl switch
        {
            MxcsrRoundingControl.Even => Math.Truncate(value),
            MxcsrRoundingControl.TowardNegativeInfinity => Math.Round(value),
            MxcsrRoundingControl.TowardPositiveInfinity => Math.Floor(value),
            MxcsrRoundingControl.Truncate => Math.Ceiling(value),
            _ => 0D
        };
    }
}
