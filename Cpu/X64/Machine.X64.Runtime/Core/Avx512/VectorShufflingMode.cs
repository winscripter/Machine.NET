namespace Machine.X64.Runtime.Core.Avx512;

/// <summary>
/// Represents the vector shuffling mode.
/// </summary>
public enum VectorShufflingMode
{
    /// <summary>
    /// Lower half of the first vector.
    /// </summary>
    LowFirst,

    /// <summary>
    /// Upper half of the first vector.
    /// </summary>
    HighFirst,

    /// <summary>
    /// Lower half of the second vector.
    /// </summary>
    LowSecond,

    /// <summary>
    /// Upper half of the second vector.
    /// </summary>
    HighSecond,
}
