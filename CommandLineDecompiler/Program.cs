using Iced.Intel;

if (args.Length != 2)
{
    Console.WriteLine("Pass two args: file name and formatter (which can be Nasm|Masm|Intel|Gas).");
    return;
}

string fileName = args[0];
if (!File.Exists(fileName))
{
    Console.WriteLine($"File not found: '{fileName}'");
    return;
}

Console.Write("Enter bitness (16/32/64): ");
int bitness = Console.ReadLine()?.ToLowerInvariant().Trim() switch
{
    "16" => 16,
    "32" => 32,
    "64" => 64,
    null or _ => throw new InvalidOperationException("Bad bitness")
};

string[] formatters = ["nasm", "masm", "intel", "gas"];
string formatter = args[1];
if (!formatters.Contains(formatter.ToLower()))
{
    Console.WriteLine($"No such formatter: {formatter}. Try Nasm, Masm, Intel, or Gas.");
    return;
}

var decoder = Decoder.Create(bitness, File.ReadAllBytes(fileName));

Formatter? fmt = formatter.ToLower() switch
{
    "nasm" => new NasmFormatter(),
    "masm" => new MasmFormatter(),
    "intel" => new IntelFormatter(),
    "gas" => new GasFormatter(),
    _ => null
};

if (fmt is null)
{
    Console.WriteLine("Invalid formatter");
    return;
}

var output = new FormatterOutputImpl();
foreach (var instr in decoder)
{
    output.List.Clear();
    fmt.Format(instr, output);

    foreach (var (text, kind) in output.List)
    {
        Console.ForegroundColor = GetColor(kind);
        Console.Write(text);
    }
    Console.WriteLine();
}
Console.ResetColor();

static ConsoleColor GetColor(FormatterTextKind kind)
{
    return kind switch
    {
        FormatterTextKind.Directive or FormatterTextKind.Keyword => ConsoleColor.Yellow,
        FormatterTextKind.Prefix or FormatterTextKind.Mnemonic => ConsoleColor.Red,
        FormatterTextKind.Register or FormatterTextKind.LabelAddress or FormatterTextKind.FunctionAddress => ConsoleColor.Magenta,
        FormatterTextKind.Number => ConsoleColor.Green,
        _ => ConsoleColor.White,
    };
}

internal sealed class FormatterOutputImpl : FormatterOutput
{
    public readonly List<(string text, FormatterTextKind kind)> List = [];
    public override void Write(string text, FormatterTextKind kind) => List.Add((text, kind));
}
