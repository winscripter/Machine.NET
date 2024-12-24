using ConsoleDebugger;

Console.Write("Enter path to the file that contains the bytecode: ");
string? bytecodeFilePath = Console.ReadLine();

Console.WriteLine("Bitness (64/32/16): ");
string? bitness = Console.ReadLine();

Console.WriteLine("Value of RSP (invalid value autodefaults to 0x100): ");
ulong rsp = ulong.TryParse(Console.ReadLine(), out ulong result) ? result : 0x100uL;

Debugger.Run(bytecodeFilePath, bitness, rsp);
