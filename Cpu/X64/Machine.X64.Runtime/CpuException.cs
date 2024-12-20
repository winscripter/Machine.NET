namespace Machine.X64.Runtime;

/// <summary>
/// A CPU error.
/// </summary>
public sealed class CpuException : Exception
{
    public CpuException()
    {
    }

    public CpuException(string? message) : base(message)
    {
    }

    public CpuException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}