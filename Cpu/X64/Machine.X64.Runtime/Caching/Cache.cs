using System.Diagnostics;

namespace Machine.X64.Runtime.Caching;

/// <summary>
/// Represents a cache that mimics the caching system used in modern x64 processors.
/// </summary>
[DebuggerDisplay($"{{{nameof(DebuggerDisplay)}}}")]
public sealed class Cache // Seal the cache class to prevent inheritance and introduce better performance.
{
    private readonly int _lineSize;
    private readonly int _numLines;
    private readonly CacheLine[] _lines;

    /// <summary>
    /// Initializes a new instance of the <see cref="Cache"/> class with the specified line size and number of lines.
    /// </summary>
    /// <param name="lineSize">The size of each cache line.</param>
    /// <param name="numLines">The number of cache lines.</param>
    public Cache(int lineSize, int numLines)
    {
        _lineSize = lineSize;
        _numLines = numLines;
        _lines = new CacheLine[numLines];

        for (int i = 0; i < numLines; i++)
        {
            _lines[i] = new CacheLine(lineSize);
        }
    }

    /// <summary>
    /// Represents the number of cache lines.
    /// </summary>
    public int Lines => _numLines;

    /// <summary>
    /// Retrieves the cache line at the specified index.
    /// </summary>
    /// <param name="index">The index of the cache line to retrieve.</param>
    /// <returns>The cache line at the specified index.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the index is greater than the number of cache lines.</exception>
    public CacheLine GetCacheLine(int index)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, _numLines - 1, nameof(index));
        return _lines[index];
    }

    /// <summary>
    /// Reads data from the cache at the specified address.
    /// </summary>
    /// <param name="address">The address to read from.</param>
    /// <returns>The data read from the cache, or null if there is a cache miss.</returns>
    public CacheReadResult Read(ulong address)
    {
        ulong tag = address / (ulong)_lineSize;
        int index = (int)(address % (ulong)_numLines);

        CacheLine line = _lines[index];
        if (line.Valid && line.Tag == tag)
        {
            // In the context when we read from cache, we should
            // return the data that was read from the cache, and set
            // the address and lineSize constructor parameters to 0 since
            // we're not manually reading from memory and the data was returned
            // from the cache.
            var crr = new CacheReadResult(0, 0, ReadResult.ReadFromCache, line.Data);
            return crr;
        }

        // Cache miss, so fetch from memory. We provide 
        // the address and lineSize to the constructor to indicate
        // that the data needs to be read from memory. Set the readResult
        // parameter to ReadResult.ReadFromMemory to let the caller know
        // that data should be fetched from memory. Do not provide the data
        // since we don't know it yet - the memory is stored elsewhere.
        var cacheReadResult = new CacheReadResult(address, _lineSize, ReadResult.ReadFromMemory);
        return cacheReadResult;
    }

    /// <summary>
    /// Writes data to the cache at the specified address.
    /// </summary>
    /// <param name="address">The address to write to.</param>
    /// <param name="data">The data to write to the cache.</param>
    public WriteResult Write(ulong address, byte[] data)
    {
        ulong tag = address / (ulong)_lineSize;
        int index = (int)(address % (ulong)_numLines);

        CacheLine line = _lines[index];
        if (line.Valid && line.Tag == tag)
        {
            Buffer.BlockCopy(data, 0, line.Data, 0, _lineSize);
            line.Dirty = true;
            return WriteResult.WrittenToCache;
        }
        else
        {
            line.Tag = tag;
            Buffer.BlockCopy(data, 0, line.Data, 0, _lineSize);
            line.Valid = true;
            line.Dirty = true;

            return WriteResult.WriteToMemory;
        }
    }

    private string DebuggerDisplay => $"L1: {_numLines * _lineSize / 1024}KB, L2: {(_numLines * _lineSize * 2) / (1024 * 1024)}MB, L3: {(_numLines * _lineSize * 3) / (1024 * 1024)}MB";
}
