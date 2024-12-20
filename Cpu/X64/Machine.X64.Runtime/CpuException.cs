namespace Machine.X64.Runtime;

/// <summary>
/// A CPU error.
/// </summary>
public sealed class CpuException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CpuException"/> class.
    /// </summary>
    public CpuException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CpuException"/> class.
    /// </summary>
    /// <param name="message">Exception message (if any)</param>
    public CpuException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CpuException"/> class.
    /// </summary>
    /// <param name="message">Exception message (if any)</param>
    /// <param name="innerException">Inner exception (if any)</param>
    public CpuException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}