namespace Machine.X64.Runtime.Caching;

/// <summary>
/// Result of the cache read operation.
/// </summary>
public readonly struct CacheReadResult
{
    /// <summary>
    /// Address in memory where the data will be read from.
    /// </summary>
    public ulong Address { get; }

    /// <summary>
    /// Length of the data to be read.
    /// </summary>
    public int LineSize { get; }

    /// <summary>
    /// General result of the read operation.
    /// </summary>
    public ReadResult ReadResult { get; }

    /// <summary>
    /// The data that was read from the cache. This is null if the data needs to
    /// be read from memory, which is typically performed by the caller of the
    /// Read method.
    /// </summary>
    public byte[]? Data { get; }

    internal CacheReadResult(ulong address, int lineSize, ReadResult readResult, byte[]? data = null)
    {
        Address = address;
        LineSize = lineSize;
        ReadResult = readResult;
        Data = data;
    }
}
