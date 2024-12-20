namespace Machine.X64.Runtime;

/// <summary>
/// The repeat prefix.
/// </summary>
public enum RepeatPrefix : byte
{
    /// <summary>
    /// None.
    /// </summary>
    None = 0,

    /// <summary>
    /// REPE
    /// </summary>
    Repe,

    /// <summary>
    /// !REPE
    /// </summary>
    Repne,

    /// <summary>
    /// REPZ
    /// </summary>
    Repz,

    /// <summary>
    /// !REPZ
    /// </summary>
    Repnz
}
