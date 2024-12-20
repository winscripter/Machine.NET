namespace Machine.X64.Runtime.Errors;

internal static class StaticErrors
{
    public static readonly GeneralProtectionFault GeneralProtectionFault = new();
    public static readonly DeviceNotAvailable DeviceUnavailable = new();
    public static readonly ZeroDivision ZeroDivision = new();
    public static readonly UnboundIndex UnboundIndex = new();
}
