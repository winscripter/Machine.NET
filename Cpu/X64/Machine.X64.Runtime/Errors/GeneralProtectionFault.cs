namespace Machine.X64.Runtime.Errors;

internal sealed class GeneralProtectionFault : IErrorRecord
{
    public string? DisplayName => "General Protection Fault";

    public ulong ErrorCode => 0;

    public byte Interrupt => 13;
}
