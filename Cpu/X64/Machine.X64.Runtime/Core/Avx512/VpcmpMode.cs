namespace Machine.X64.Runtime.Core.Avx512;

/// <summary>
/// Mode for the VPCMP*/VPCMPU* instructions.
/// </summary>
public enum VpcmpMode
{
    /// <summary>
    /// Value is identical.
    /// </summary>
    Equal,

    /// <summary>
    /// Value 1 is less than value 2.
    /// </summary>
    LessThan,

    /// <summary>
    /// Value 1 is either less than value 2 or they are both equal.
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// Both values are zero.
    /// </summary>
    False,

    /// <summary>
    /// Value 1 and value 2 are different in any way.
    /// </summary>
    NotEqual,

    /// <summary>
    /// Value 1 is either greater than value 2 or they are both equal.
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// Value 1 is greater than value 2.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Both values are one.
    /// </summary>
    True
}
