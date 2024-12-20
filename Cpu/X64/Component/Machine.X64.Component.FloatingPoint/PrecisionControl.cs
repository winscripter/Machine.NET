namespace Machine.X64.Component;

/// <summary>
/// Specifies the precision of the FPU.
/// </summary>
public enum PrecisionControl
{
    /// <summary>
    /// 32-bit REAL4 floating-point values.
    /// </summary>
    Real4,
    
    /// <summary>
    /// This value is invalid.
    /// </summary>
    Invalid,
    
    /// <summary>
    /// 64-bit REAL8 floating-point values.
    /// </summary>
    Real8,
    
    /// <summary>
    /// 80-bit REAL10 floating-point values.
    /// </summary>
    Real10
}
