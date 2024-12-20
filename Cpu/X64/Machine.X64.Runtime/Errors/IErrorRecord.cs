namespace Machine.X64.Runtime.Errors;

/// <summary>
/// Represents a general CPU exception.
/// </summary>
public interface IErrorRecord
{
    /// <summary>
    /// Friendly name (if any).
    /// </summary>
    string? DisplayName { get; }
    
    /// <summary>
    /// The error code.
    /// </summary>
    ulong ErrorCode { get; }

    /// <summary>
    /// The error interrupt.
    /// </summary>
    byte Interrupt { get; }
}
