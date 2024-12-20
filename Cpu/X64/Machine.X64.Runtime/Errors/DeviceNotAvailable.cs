namespace Machine.X64.Runtime.Errors;

internal sealed class DeviceNotAvailable : IErrorRecord
{
    public string? DisplayName => "The device vital to the executability of this instruction is unavailable.";

    public ulong ErrorCode => 0;

    public byte Interrupt => 7;
}
