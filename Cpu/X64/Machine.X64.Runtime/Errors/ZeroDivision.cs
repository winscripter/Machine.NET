namespace Machine.X64.Runtime.Errors;

internal sealed class ZeroDivision : IErrorRecord
{
    public string? DisplayName => "Division by zero";

    public ulong ErrorCode => 0;

    public byte Interrupt => 13;
}
