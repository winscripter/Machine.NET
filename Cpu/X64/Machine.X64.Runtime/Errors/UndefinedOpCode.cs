namespace Machine.X64.Runtime.Errors;

internal sealed class UndefinedOpCode : IErrorRecord
{
    public string? DisplayName => "The operation code is unknown";

    public ulong ErrorCode => 0;

    public byte Interrupt => 6;
}
