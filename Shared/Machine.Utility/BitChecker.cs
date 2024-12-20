namespace Machine.Utility;

internal static class BitChecker
{
    public static bool IsParity(double value)
    {
        ReadOnlySpan<bool> bits = BitManager.GetBitsBigEndian(value);
        return bits.ToArray().Count(x => x == true) % 2 == 0;
    }
}
