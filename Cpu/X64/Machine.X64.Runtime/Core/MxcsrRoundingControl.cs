namespace Machine.X64.Runtime.Core;

/// <summary>
/// The rounding control of the RC (Rounding control) exception mask in the MXCSR register.
/// </summary>
public enum MxcsrRoundingControl : byte
{
    /// <summary>
    /// Round to nearest.
    /// </summary>
    Even,

    /// <summary>
    /// Round down.
    /// </summary>
    TowardNegativeInfinity,

    /// <summary>
    /// Round up.
    /// </summary>
    TowardPositiveInfinity,

    /// <summary>
    /// Round toward zero.
    /// </summary>
    Truncate,
}
