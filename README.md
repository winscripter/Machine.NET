# Machine.NET

Table of contents:
1. [Machine.NET](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#machinenet)
2. [Supported features](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#supported-features)
   - [x64](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#x64)
3. [&#91;!&#93; Limitations &#91;!&#93;](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#-limitations-)
   - [x64](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#x64-1)
4. [Usage](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#usage)
5. [Building](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#building)
6. [Packages](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#packages)
7. [License](https://github.com/winscripter/Machine.NET?tab=readme-ov-file#license)

<img align="right" width="160" height="160" src="Images/Icon/MachineDotNetImage.Black.png">

Use Machine.NET if you'd like to create a platform-independent virtual machine with just C#.

Right now, only X64 is supported, as well as the Intel 8253, 8259 chipset and HPET. Currently I'm focusing on solving
existing problems with the X64 emulator rather than adding more possibilities. New features will be added once the X64 emulator begins to show success, **however**,
you can still suggest new features if you'd like.

Please support the project by placing a star on this repository.

# Supported features
### X64
- 731 instructions are supported, as of December 24, 2024.
- 64-bit mode
- Many 8086, 80186, 80386 and 80486 instructions
- Partially working protected mode
- SSE, SSE2, SSE3, SSSE3, SSE4.1, and SSE4.2 (Mostly working)
- AVX and AVX512:
  - AVX512-4FMAPS
  - AVX512-4VNNIW
  - AVX512-BITALG
  - AVX512-BW
  - AVX512-CD
  - AVX512-DQ
  - AVX512-ER
  - AVX512-F
  - AVX512-FP16 (Partial)
  - AVX512-GFNI
  - AVX512-VBMI
  - AVX512-VPOPCNTDQ
  - AVX512-VPCLMULQDQ
- ADX, ABM, BMI1, CMOV, CX8, FSGSBASE, MOVBE, MOVDIR64B, MOVDIRI, MSR, PKU, RDRAND, RDSEED, SMAP, TSC, WRMSRNS
- FMA3 (Fused Multiply Add 3)
  - FMA4 is not supported because it's no longer used in real world CPUs anymore

# [!] Limitations [!]
### X64
- About 10-25% of instructions are not implemented on average
- No support for paging
- No support for caching
- Partial support for protected mode

# Usage
When you use Machine.NET, the NuGet package called Iced is installed too.
This is a popular package for decoding and encoding instructions, and it's used in Machine.NET to decode instructions.

To begin, use Iced.Intel to assemble the instructions we need.
In our case, it is:
```
mov rcx, 150
rep add rax, 4
```
That would be:
```csharp
using Iced.Intel;
using Machine.X64.Runtime;
using static Iced.Intel.AssemblerRegisters;

var assembler = new Assembler(64);
assembler.mov(rcx, 150);
assembler.rep.add(rax, 4);

var stream = new MemoryStream();
var streamCodeWriter = new StreamCodeWriter(stream);

assembler.Assemble(streamCodeWriter, rip: 0uL);

stream.Position = 0;
var reader = new StreamCodeReader(stream);
var decoder = Decoder.Create(64, reader);
decoder.IP = 0;
var instrs = new List<Instruction>();
while (stream.Position < stream.Length)
{
    decoder.Decode(out var instr);
    instrs.Add(instr);
}
```

And now, simply create a new instance of the CpuRuntime class. You can pass the amount of memory (in bytes) and the number of I/O ports. In our case, this is 64KB memory and 8 I/O ports:
```csharp
var runtime = new CpuRuntime(memorySize: 65536, ioPortCount: 8);
```
You can invoke the `.Run(in Instruction)` method on `CpuRuntime` to invoke an instruction. Let's invoke all instructions:
```csharp
foreach (var instr in instrs)
{
    runtime.Run(in instr);
}
```

The `.Run` method can be slightly limiting in case you want to support jump and branch instructions. In that case, it's possible to load direct bytecode
into RAM and load it from there:
```cs
var cpu = new CpuRuntime(ioPortCount: 8);
ulong x = 0uL;
cpu.IOPorts[1] = new InputOutputPort(
    read: () =>
    {
        return 1234uL;
    },
    write: (value) =>
    {
        x = value;
    });
byte[] code = CodeGen.MakeBranchTestCode_1();

cpu.LoadProgram(code, 0uL);
cpu.ProcessorRegisters.Cs = 0;
cpu.ProcessorRegisters.Rip = 0;
cpu.Use8086Compatibility();
cpu.SetRsp(0x400uL);

try
{
    cpu.RunUntilNotBusy(35);
}
catch (ArithmeticException)
{
    throw new InvalidOperationException(cpu.LastOrExecutingInstruction.Code.ToString());
}

Assert.Equal(42uL, x);

static class CodeGen
{
    public static byte[] MakeBranchTestCode_1()
    {
        var assembler = new Assembler(64);
        Label lblA = assembler.CreateLabel("A");
        Label lblC = assembler.CreateLabel("C");
        Label lblB = assembler.CreateLabel("B");

        assembler.Label(ref lblA);
        assembler.mov(ax, 42);
        assembler.@out(1, ax);
        assembler.call(lblB);

        assembler.Label(ref lblC);
        assembler.mov(ax, bx);
        assembler.@out(1, ax);
        assembler.hlt();

        assembler.Label(ref lblB);
        assembler.mov(bx, ax);
        assembler.call(lblC);

        return Assemble(assembler);
    }

    private static byte[] Assemble(Assembler assembler)
    {
        using var memoryStream = new MemoryStream();
        assembler.Assemble(new StreamCodeWriter(memoryStream), 0uL);
        return memoryStream.ToArray();
    }
}
```
Indeed, x is 42. The .RunUntilNotBusy method begins running instructions from memory at CS:RIP or
just RIP by default. It has two overloads: one that takes int and one that doesn't. The one that does
represents the maximum amount of instructions it should run, which is safer to use if you're worrying in the
case of infinite loop. The one that doesn't take any parameters will keep running until the HLT instruction.

You can also access the `.ProcessorRegisters` property of the `CpuRuntime` class to inspect CPU registers and flags and even modify them at any time.
To see the result, we'll view the `rax` register:
```csharp
Console.WriteLine(runtime.ProcessorRegisters.Rax);
```
This results in 600, which is correct.

To attach external devices, you can make your own I/O port and put whatever you want in READ/WRITE operations (yes, even creating a new
window and displaying it, if you want).
```cs
var cpu = new CpuRuntime(ioPortCount: 8);
ulong x = 42uL;
cpu.IOPorts[1] = new InputOutputPort(
    read: () =>
    {
        return 1234uL;
    },
    write: (value) =>
    {
        x = value;
    });
```
For example, if we execute the following code on the emulated CPU:
```asm
mov eax, 7777
out 1, eax
in eax, 1
```
Then, you can see that the CPU sent 7777 to I/O port indexed 1 (I/O ports are indexed starting from 0),
and EAX is equal to 1234 (check out [this unit test, it's pretty cool](UnitTests/X64Tests/IOPortTests.cs)):
```cs
Assert.Equal(7777uL, x);
Assert.Equal(1234uL, cpu.ProcessorRegisters.Eax);

// No failures
```

# Building
To build Machine.NET, you need to have .NET 8.0 installed. You can download it from the official .NET website.

If you prefer with Visual Studio:

1. Download Visual Studio 2022 with the workload ".NET Desktop Development". If you already have it:
   - Open Visual Studio Installer
   - Next to the version of Visual Studio that you'll use to build Machine.NET, click Modify.
   - Check the ".NET Desktop Development" workload.
   - Click Modify. This will take a while. You'll need ample space on your hard drive and a good network connection.
2. If you already have Visual Studio 2022 installed, or you finished installing it: 
   - Clone this repository. You can do this via git or by downloading the ZIP file via GitHub (in the root of the repo, click Code -&gt; Download ZIP).
   - Open the solution file (Machine.NET.sln) in Visual Studio.
   - Right click on the solution in Solution Explorer (Ctrl + Alt + L) and click Build Solution.
       - Ensure that you have plenty amount of available RAM on your PC to build Machine.NET.

If you prefer with .NET CLI:
    
1. Clone this repository. You can do this via git or by downloading the ZIP file via GitHub (in the root of the repo, click Code -&gt; Download ZIP).
2. Open a terminal in the cloned repository.
3. Type `dotnet build`. Or type `dotnet build -c Release` to build in release mode (e.g. if you want to use Machine.NET in real world apps with optimization enabled).

# Packages
| Library name | NuGet URL | Source code on this repo |
| ------------ | --------- | ------------------------ |
| Machine.X64.Component.Registers | [![version](https://img.shields.io/badge/Package-1.0.1-blue)](https://nuget.org/packages/Machine.X64.Component.Registers) | [Click to redirect to source](https://github.com/winscripter/Machine.NET/tree/master/Cpu/X64/Component/Machine.X64.Component.Registers)

# License
MIT License. Copyright (c) winscripter, 2023-2024.
