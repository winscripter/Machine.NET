namespace Machine.X64.Runtime.Caching;

/// <summary>
/// Specifies the result of the cache read operation.
/// </summary>
public enum ReadResult
{
    /// <summary>
    /// The data was read from the cache.
    /// </summary>
    ReadFromCache,

    /// <summary>
    /// The data was will be read from memory.
    /// </summary>
    ReadFromMemory
}
