namespace Machine.X64.Runtime.Caching;

/// <summary>
/// Cache line implementation in C#, mimicking those used in modern processors.
/// </summary>
public sealed record CacheLine
{
    /// <summary>
    /// Gets or sets the tag associated with the cache line.
    /// </summary>
    public ulong Tag { get; set; }

    /// <summary>
    /// Gets or sets the data stored in the cache line.
    /// </summary>
    public byte[] Data { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cache line is valid.
    /// </summary>
    public bool Valid { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cache line is dirty.
    /// </summary>
    public bool Dirty { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CacheLine"/> class with the specified line size.
    /// </summary>
    /// <param name="lineSize">The size of the cache line.</param>
    public CacheLine(int lineSize)
    {
        Data = new byte[lineSize];
        Valid = false;
        Dirty = false;
    }
}
