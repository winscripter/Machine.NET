namespace RegistersXmlToCSharp;

internal sealed class CommandLineIterator
{
    private readonly string[] _arguments;
    private int _offset;

    public CommandLineIterator(string[] arguments)
    {
        if (arguments.Length == 0)
            throw new ArgumentException("Argument length cannot be zero", nameof(arguments));

        _arguments = arguments;
        _offset = 0;
    }

    public string Current => _arguments[_offset];

    public int Offset => _offset;

    public bool MoreLeft => _offset < _arguments.Length - 1;

    public void Advance()
    {
        _offset++;
    }
}
