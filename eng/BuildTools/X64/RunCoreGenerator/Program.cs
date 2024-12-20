using System.Text;

if (args.Length != 1)
{
    Console.WriteLine("Error: Expected one command line argument: the directory");
    return;
}

string directory = args[0];
if (!Directory.Exists(directory))
{
    Console.WriteLine("Error: Directory does not exist");
    return;
}

string[] recursiveFiles = [.. Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
                                   .Where(file => file.EndsWith(".cs"))
                                   .Select(file => file.Split('\\').Last())
                                   .OrderBy(file => file)];

var stringBuilder = new StringBuilder($@"using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{{
    private void RunCore(in Instruction instruction)
    {{
        switch (instruction.Mnemonic)
        {{");

foreach (string file in recursiveFiles)
{
    string withoutDotCs = file.Split('.')[0];
    string name = !withoutDotCs.StartsWith("Vfm") && !withoutDotCs.StartsWith("Vfnm") ? withoutDotCs.ToLower() : withoutDotCs;
    stringBuilder.AppendLine($"            case Mnemonic.{withoutDotCs}: {name}(in instruction); break;");
}

stringBuilder.Append(@"
        }
    }
}");

Console.WriteLine(stringBuilder.ToString());

