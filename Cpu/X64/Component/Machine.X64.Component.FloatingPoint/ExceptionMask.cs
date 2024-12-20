namespace Machine.X64.Component;

/// <summary>
/// Specifies floating point exceptions.
/// </summary>
[Flags]
public enum ExceptionMask
{
    /// <summary>
    /// No exceptions.
    /// </summary>
    Clear = 0,

    /// <summary>
    /// The floating point invalid operation exception.
    /// </summary>
    InvalidOperation = 1,

    /// <summary>
    /// The operand denormalized exception.
    /// </summary>
    Denormalized = 2,

    /// <summary>
    /// The floating point divide by zero exception.
    /// </summary>
    ZeroDivide = 4,

    /// <summary>
    /// The floating point overflow exception.
    /// </summary>
    Overflow = 8,

    /// <summary>
    /// The floating point underflow exception.
    /// </summary>
    Underflow = 16,

    /// <summary>
    /// The floating point precision exception.
    /// </summary>
    Precision = 32
}
