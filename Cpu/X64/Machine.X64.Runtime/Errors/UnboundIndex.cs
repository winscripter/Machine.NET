namespace Machine.X64.Runtime.Errors;

internal sealed class UnboundIndex : IErrorRecord
{
    public string? DisplayName => "Unbound Index";

    public ulong ErrorCode => 0;

    public byte Interrupt => 5;
}
