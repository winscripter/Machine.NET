namespace Machine.X64.Component;

/// <summary>
/// The type of the segment descriptor.
/// </summary>
[Flags]
public enum SegmentDescriptorType : byte
{
    /// <summary>
    /// Differentiates between code and data segments.
    /// </summary>
    CodeDataSegment = 1,

    /// <summary>
    /// Indicates whether the segment contains executable code.
    /// </summary>
    Executable = 2,

    /// <summary>
    /// For data segments, indicates if the segment grows downwards.
    /// </summary>
    ExpandDown = 4,

    /// <summary>
    /// Indicates if the data segment is writable or if the code segment is readable.
    /// </summary>
    WritableReadable = 8,

    /// <summary>
    /// Indicates if the segment has been accessed.
    /// </summary>
    Accessed = 16,
}
