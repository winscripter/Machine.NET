namespace Machine.X64.Component;

/// <summary>
/// Specifies the rounding mode used by the FPU.
/// </summary>
public enum RoundingControl
{
    /// <summary>
    /// Round to nearest integer.
    /// </summary>
    Nearest,
    
    /// <summary>
    /// Round towards -infinity.
    /// </summary>
    Down,
    
    /// <summary>
    /// Round towards +infinity.
    /// </summary>
    Up,
    
    /// <summary>
    /// Round towards zero.
    /// </summary>
    Truncate
}
