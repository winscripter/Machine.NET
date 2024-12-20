namespace Machine.X64.Runtime.Caching;

/// <summary>
/// Represents the result of a write operation to the cache.
/// </summary>
public enum WriteResult
{
    /// <summary>
    /// The data was written to the cache.
    /// </summary>
    WrittenToCache,

    /// <summary>
    /// The data needs to be written to memory.
    /// </summary>
    WriteToMemory
}
