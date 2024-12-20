namespace RegistersXmlToCSharp;

internal sealed class CommandLine
{
    public string Source { get; set; }
    public string Target { get; set; }

    internal CommandLine(string source, string target)
    {
        Source = source;
        Target = target;
    }

    public static CommandLine Read(string[] args)
    {
        var iterator = new CommandLineIterator(args);
        string? source = null;
        string? target = null;

        switch (iterator.Current.ToLowerInvariant())
        {
            case "--source":
            case "/i":
                iterator.Advance();
                source = iterator.Current;
                iterator.Advance();
                break;

            case "--target":
            case "/o":
                iterator.Advance();
                target = iterator.Current;
                iterator.Advance();
                break;

            default:
                throw new InvalidOperationException($"Unrecognized argument '{iterator.Current}'.  Use the '/?' parameter for more info.");
        }

        switch (iterator.Current.ToLowerInvariant())
        {
            case "--source":
            case "/i":
                if (source is not null)
                {
                    throw new InvalidOperationException("Attempted to set source twice");
                }
                iterator.Advance();
                source = iterator.Current;
                iterator.Advance();
                break;

            case "--target":
            case "/o":
                if (target is not null)
                {
                    throw new InvalidOperationException("Attempted to set target twice");
                }
                iterator.Advance();
                target = iterator.Current;
                iterator.Advance();
                break;

            default:
                throw new InvalidOperationException($"Unrecognized argument '{iterator.Current}'.  Use the '/?' parameter for more info.");
        }

        if (source is null)
        {
            throw new InvalidOperationException("Source is missing.");
        }

        if (target is null)
        {
            throw new InvalidOperationException("Target is missing.");
        }

        return new CommandLine(source, target);
    }

    public static void Drive(string[] args)
    {
        if (args.Length == 0)
        {
            DisplayHelp();
            return;
        }

        if (args.Length == 1 &&
            args.First() == "/?")
        {
            DisplayHelp();
            return;
        }

        CommandLine commandLine;
        try
        {
            commandLine = Read(args);
        }
        catch (Exception e)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(e.Message);

            Console.ForegroundColor = foregroundColor;
            return;
        }

        try
        {
            string inputFile = File.ReadAllText(commandLine.Source);
            string result = CodeGenerator.Generate(Reader.Load(inputFile));
            File.WriteAllText(commandLine.Target, result);
        }
        catch (Exception e)
        {
            var foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(e.Message);

            Console.ForegroundColor = foregroundColor;
            return;
        }

        Console.WriteLine("Operation completed successfully.");
    }

    private static void DisplayHelp()
    {
        Console.WriteLine("USAGE: RegistersXmlToCSharp");
        Console.WriteLine();
        Console.WriteLine("Description:");
        Console.WriteLine("    Convert Registers.xml to C# class or interface.");
        Console.WriteLine();
        Console.WriteLine("--source <file>   Specify the input");
        Console.WriteLine("/i <file>         file to process. Its");
        Console.WriteLine("                  contents are read then");
        Console.WriteLine("                  processed.");
        Console.WriteLine("--target <file>   Specify the destination");
        Console.WriteLine("/o <file>         file. Results are written");
        Console.WriteLine("                  here.");
        Console.WriteLine("--help            Display this message to");
        Console.WriteLine("/?                provide additional context");
        Console.WriteLine("                  about what this program is");
        Console.WriteLine("                  and how it works.");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("    RegistersXmlToCSharp --source Registers.xml --target Result.cs");
        Console.WriteLine("    RegistersXmlToCSharp /?");
    }
}
