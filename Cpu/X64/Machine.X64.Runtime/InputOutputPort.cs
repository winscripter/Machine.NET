namespace Machine.X64.Runtime;

/// <summary>
/// Simulates an I/O port.
/// </summary>
public sealed class InputOutputPort
{
    /// <summary>
    /// A function that returns the value of the port. READ operations on this
    /// I/O port can be any, but the return value must be a 64-bit unsigned integer.
    /// </summary>
    public Func<ulong> Read { get; set; }

    /// <summary>
    /// A function that writes a value to the port. WRITE operations on this
    /// I/O port can be any, but the input value must be a 64-bit unsigned integer.
    /// </summary>
    public Action<ulong> Write { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InputOutputPort"/> class.
    /// </summary>
    /// <param name="read">Custom READ operations for the I/O port.</param>
    /// <param name="write">Custom WRITE operations for the I/O port.</param>
    public InputOutputPort(Func<ulong> read, Action<ulong> write)
    {
        this.Read = read;
        this.Write = write;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InputOutputPort"/> class, setting
    /// read and write operations to default values (e.g. READ is hardcoded to return 0,
    /// and WRITE does not perform any operations).
    /// </summary>
    public InputOutputPort()
    {
        this.Read = () => 0uL;
        this.Write = _ => { };
    }
}
