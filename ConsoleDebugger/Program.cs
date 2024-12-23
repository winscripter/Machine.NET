using ConsoleDebugger;

Console.Write("Enter path to the file that contains the bytecode: ");
string? bytecodeFilePath = Console.ReadLine();

Console.WriteLine("Bitness (64/32/16): ");
string? bitness = Console.ReadLine();

Debugger.Run(bytecodeFilePath, bitness);
