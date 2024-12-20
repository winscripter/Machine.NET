# Machine.NET

<img align="right" width="160" height="160" src="Images/Icon/MachineDotNetImage.Black.png">

Welcome to the Machine.NET repository! This is an experimental project to allow
you to run unmanaged code in a managed environment. This project is still in its early stages, so there may
be more bugs than unfinished features. If you find any bugs, please report them in the issues tab - this project
will be botched without your help.

An example where this might actually be useful is Blazor WebAssembly. It's quite limiting but
for a reason. It's impossible to run unmanaged apps or operating systems (like apps written in C/C++) in a
client-side browser environment. This project aims to solve that problem, by emulating
the CPU and devices - X64, ARM, RISC-V, etc. However, note that only X64 is supported right now - see the paragraph below.

Right now, only X64 is supported, as well as the Intel 8253, 8259 chipset and HPET. Currently I'm focusing on solving
existing problems with the X64 emulator rather than adding more possibilities. New features will be added once the X64 emulator begins to show success

# Supported features
### X64
- 720 instructions are supported, as of December 20, 2024.
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
You can also access the `.ProcessorRegisters` property of the `CpuRuntime` class to inspect CPU registers and flags and even modify them at any time.
To see the result, we'll view the `rax` register:
```csharp
Console.WriteLine(runtime.ProcessorRegisters.Rax);
```
This results in 600, which is correct.

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
3. Type `dotnet build`. Or type `dotnet build -c Release` to build in release mode (e.g. if you want to use Machine.NET in real world apps).

# License
MIT License. Copyright (c) winscripter, 2023-2024.
