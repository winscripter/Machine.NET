using Machine.Utility;

namespace Machine.X64.Runtime.Core.Avx512;

/// <summary>
/// Simulates comparison of two elements in the VPCMP[?]/VPCMPU[?] instructions.
/// </summary>
public static class VpcmpComparer
{
    /// <summary>
    /// Performs comparison between two signed doublewords. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, int element1, int element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }

    /// <summary>
    /// Performs comparison between two unsigned doublewords. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, uint element1, uint element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }

    /// <summary>
    /// Performs comparison between two signed quadwords. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, long element1, long element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }

    /// <summary>
    /// Performs comparison between two unsigned quadwords. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, ulong element1, ulong element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }

    /// <summary>
    /// Performs comparison between two signed bytes. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, sbyte element1, sbyte element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }

    /// <summary>
    /// Performs comparison between two unsigned bytes. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, byte element1, byte element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;

        }
    }

    /// <summary>
    /// Performs comparison between two signed words. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, short element1, short element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }

    /// <summary>
    /// Performs comparison between two unsigned words. If the result is true, the bit
    /// indexed by the parameter <paramref name="bit"/> at <paramref name="kreg"/> is set to
    /// 1; otherwise it is set to 0. Comparison type is specified by the <paramref name="mode"/>
    /// method parameter.
    /// </summary>
    /// <param name="kreg">The unsigned 64-bit integer that will have the given bit altered depending on the result of the comparison. This is typically one of the K registers.</param>
    /// <param name="bit">The index of the bit to alter on the <paramref name="kreg"/> parameter depending on the result of the comparison.</param>
    /// <param name="element1">The first element to compare.</param>
    /// <param name="element2">The second element to compare.</param>
    /// <param name="mode">The mode of the comparison operation.</param>
    public static void Compare(ref ulong kreg, int bit, ushort element1, ushort element2, VpcmpMode mode)
    {
        switch (mode)
        {
            case VpcmpMode.Equal:
                BitUtilities.SetBit(ref kreg, bit, element1 == element2);
                break;

            case VpcmpMode.LessThan:
                BitUtilities.SetBit(ref kreg, bit, element1 < element2);
                break;

            case VpcmpMode.GreaterThan:
                BitUtilities.SetBit(ref kreg, bit, element1 > element2);
                break;

            case VpcmpMode.LessThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 <= element2);
                break;

            case VpcmpMode.GreaterThanOrEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 >= element2);
                break;

            case VpcmpMode.False:
                BitUtilities.SetBit(ref kreg, bit, element1 == 0 && element2 == 0);
                break;

            case VpcmpMode.True:
                BitUtilities.SetBit(ref kreg, bit, element1 == 1 && element2 == 1);
                break;

            case VpcmpMode.NotEqual:
                BitUtilities.SetBit(ref kreg, bit, element1 != element2);
                break;
        }
    }
}
