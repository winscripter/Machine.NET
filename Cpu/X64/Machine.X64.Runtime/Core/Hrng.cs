namespace Machine.X64.Runtime.Core;

/// <summary>
/// Hardware random number generator.
/// </summary>
public static class Hrng
{
    private static Random _random = new(Environment.TickCount);

    /// <summary>
    /// Generates a random integer.
    /// </summary>
    /// <returns>A 64-bit unsigned integer which specifies the random integer.</returns>
    public static ulong GenerateRandom()
    {
        return (ulong)_random.NextInt64();
    }

    /// <summary>
    /// Generates a random seed. Prior to returning the random seed, the seed of
    /// the HRNG is set to the generated seed.
    /// </summary>
    /// <returns>A 64-bit unsigned integer which specifies the random seed.</returns>
    public static ulong GenerateSeed()
    {
        ulong seed = (ulong)_random.NextInt64();
        _random = new Random((int)seed);
        return seed;
    }
}
