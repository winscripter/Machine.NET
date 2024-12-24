using Iced.Intel;
using Machine.X64.Component;
using Machine.X64.Runtime;

namespace ConsoleDebugger;

public static class Debugger
{
    internal sealed class FormatterOutputImpl : FormatterOutput
    {
        public override void Write(string text, FormatterTextKind kind)
        {
            Console.Write(text + " ");
        }
    }

    private static readonly MasmFormatter _masmFormatter = new();
    private static readonly FormatterOutputImpl _formatterOutputImpl = new();

    public static void Run(string? bytecodeFilePath, string? bitness)
    {
        if (!File.Exists(bytecodeFilePath))
        {
            Console.WriteLine($"Bytecode file path '{bytecodeFilePath}' is missing.");
            return;
        }

        if (!int.TryParse(bitness, out int bitValue) && bitValue is not 64 and not 32 and not 16)
        {
            Console.WriteLine($"Invalid bitness: '{bitness}'");
            return;
        }

        RunCore(bytecodeFilePath!, bitValue);
    }

    private static void RunCore(string bytecodeFilePath, int bitness)
    {
        Console.Clear();

        var runtime = new CpuRuntime(1048576, 32, bitness);
        runtime.ProcessorRegisters.Cs = 0;
        runtime.ProcessorRegisters.Rip = 0;
        runtime.Use8086Compatibility();
        runtime.LoadProgram(File.ReadAllBytes(bytecodeFilePath), 0uL);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("CODE: " + runtime.LastOrExecutingInstruction.Code);

            Instruction lastOrExecutingInstruction = runtime.LastOrExecutingInstruction;
            _masmFormatter.Format(in lastOrExecutingInstruction, _formatterOutputImpl);

            Console.WriteLine("\nHALTED: " + !runtime.Busy);
            Console.WriteLine("LAST LENGTH: " + lastOrExecutingInstruction.Length);
            Console.WriteLine("LAST RIP: " + runtime.CurrentRip);
            Console.WriteLine();
            PrintRegisters(runtime.ProcessorRegisters, bitness);
            Console.WriteLine();
            Console.WriteLine();

            Console.Write("> ");
            string numberOfIterators = Console.ReadLine() ?? "0";

            if (!int.TryParse(numberOfIterators, out int iterationCount))
            {
                Console.WriteLine($"Invalid number of iterators: {numberOfIterators}");
                Console.WriteLine("Press any key to continue");
                _ = Console.ReadKey();
                continue;
            }

            runtime.RunUntilNotBusy(iterationCount);
        }
    }

    private static void PrintRegisters(ProcessorRegisters register, int bitness)
    {
        switch (bitness)
        {
            case 16:
                PrintRegisters16(register);
                break;

            case 32:
                PrintRegisters32(register);
                break;

            case 64:
                PrintRegisters64(register);
                break;
        }
    }

    private static void PrintRegisters64(ProcessorRegisters regs)
    {
        PrintRegisters32(regs);

        Console.Write("RAX: ");
        DisplayNumber(regs.Rax);
        Console.Write(' ');

        Console.Write("RBX: ");
        DisplayNumber(regs.Rbx);
        Console.Write(' ');

        Console.Write("RCX: ");
        DisplayNumber(regs.Rcx);
        Console.Write(' ');

        Console.Write("RDX: ");
        DisplayNumber(regs.Rdx);
        Console.Write(' ');

        Console.WriteLine();

        Console.Write("RBP: ");
        DisplayNumber(regs.Rbp);
        Console.Write(' ');

        Console.Write("RIP: ");
        DisplayNumber(regs.Rip);
        Console.Write(' ');

        Console.Write("RSI: ");
        DisplayNumber(regs.Rsi);
        Console.Write(' ');

        Console.Write("RDI: ");
        DisplayNumber(regs.Rdi);
        Console.Write(' ');

        Console.WriteLine();
    }

    private static void PrintRegisters32(ProcessorRegisters regs)
    {
        PrintRegisters16(regs);

        Console.Write("EAX: ");
        DisplayNumber(regs.Eax);
        Console.Write(' ');

        Console.Write("EBX: ");
        DisplayNumber(regs.Ebx);
        Console.Write(' ');

        Console.Write("ECX: ");
        DisplayNumber(regs.Ecx);
        Console.Write(' ');

        Console.Write("EDX: ");
        DisplayNumber(regs.Edx);
        Console.Write(' ');

        Console.WriteLine();

        Console.Write("EBP: ");
        DisplayNumber(regs.Ebp);
        Console.Write(' ');

        Console.Write("EIP: ");
        DisplayNumber(regs.Eip);
        Console.Write(' ');

        Console.Write("ESI: ");
        DisplayNumber(regs.Esi);
        Console.Write(' ');

        Console.Write("EDI: ");
        DisplayNumber(regs.Edi);
        Console.Write(' ');

        Console.WriteLine();
    }

    private static void PrintRegisters16(ProcessorRegisters regs)
    {
        Console.Write("AL: ");
        DisplayNumber(regs.Al);
        Console.Write(' ');

        Console.Write("BL: ");
        DisplayNumber(regs.Bl);
        Console.Write(' ');

        Console.Write("CL: ");
        DisplayNumber(regs.Cl);
        Console.Write(' ');

        Console.Write("DL: ");
        DisplayNumber(regs.Dl);

        Console.WriteLine();

        Console.Write("AH: ");
        DisplayNumber(regs.Ah);
        Console.Write(' ');

        Console.Write("BH: ");
        DisplayNumber(regs.Bh);
        Console.Write(' ');

        Console.Write("CH: ");
        DisplayNumber(regs.Ch);
        Console.Write(' ');

        Console.Write("DH: ");
        DisplayNumber(regs.Dh);
        Console.Write(' ');

        Console.WriteLine();

        Console.Write("AX: ");
        DisplayNumber(regs.Ax);
        Console.Write(' ');

        Console.Write("BX: ");
        DisplayNumber(regs.Bx);
        Console.Write(' ');

        Console.Write("CX: ");
        DisplayNumber(regs.Cx);
        Console.Write(' ');

        Console.Write("DX: ");
        DisplayNumber(regs.Dx);
        Console.Write(' ');

        Console.WriteLine();

        Console.Write("CS: ");
        DisplayNumber(regs.Cs);
        Console.Write(' ');

        Console.Write("DS: ");
        DisplayNumber(regs.Ds);
        Console.Write(' ');

        Console.Write("SS: ");
        DisplayNumber(regs.Ss);
        Console.Write(' ');

        Console.Write("ES: ");
        DisplayNumber(regs.Es);
        Console.Write(' ');

        Console.WriteLine();

        Console.Write("BP: ");
        DisplayNumber(regs.Bp);
        Console.Write(' ');

        Console.Write("IP: ");
        DisplayNumber(regs.Ip);
        Console.Write(' ');

        Console.Write("SI: ");
        DisplayNumber(regs.Si);
        Console.Write(' ');

        Console.Write("DI: ");
        DisplayNumber(regs.Di);
        Console.Write(' ');

        Console.WriteLine();
    }

    private static void DisplayNumber(byte value)
    {
        DisplayColored($"0x{value:X2}", ConsoleColor.Green);
    }

    private static void DisplayNumber(ushort value)
    {
        DisplayColored($"0x{value:X4}", ConsoleColor.Green);
    }

    private static void DisplayNumber(uint value)
    {
        DisplayColored($"0x{value:X8}", ConsoleColor.Green);
    }

    private static void DisplayNumber(ulong value)
    {
        DisplayColored($"0x{value:X16}", ConsoleColor.Green);
    }

    private static void DisplayColored(string whatToDisplay, ConsoleColor color)
    {
        ConsoleColor previousForegroundColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(whatToDisplay);
        Console.ForegroundColor = previousForegroundColor;
    }
}
