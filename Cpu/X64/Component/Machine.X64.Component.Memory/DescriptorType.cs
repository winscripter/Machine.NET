namespace Machine.X64.Component;

#pragma warning disable CS1591
public enum DescriptorType
{
    Segment,
    CallGate,
    TaskGate,
    InterruptGate,
    TrapGate,
    TaskSegmentSelector
}
#pragma warning restore CS1591
