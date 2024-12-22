# Machine.X64.Component.Registers.Iced
A small number of extension methods to the `ProcessorRegisters` class from [Machine.X64.Component.Registers](https://nuget.org/packages/Machine.X64.Component.Registers)
to evaluate registers based on the Iced.Intel.Register enumeration.

See the following example.
```cs
using Machine.X64.Component;
using System.Runtime.Intrinsics;
// Not using 'using Iced.Intel' in this context for clarity

// Set registers
var processorRegisters = new ProcessorRegisters();
processorRegisters.Rax = 123;
processorRegisters.Rbx = 42;
processorRegisters.Xmm5 = Vector128.Create(1F, 2F, 3F, 4F);
processorRegisters.Xmm3 = Vector128.Create(4F, 3F, 2F, 1F);

// Evaluate them
Console.WriteLine(processorRegisters.EvaluateRegisterValue32(Iced.Intel.Register.RAX)); // 123
Console.WriteLine(processorRegisters.EvaluateRegisterValue32(Iced.Intel.Register.RBX)); // 42
Console.WriteLine(processorRegisters.EvaluateXmm(Iced.Intel.Register.XMM5)); // <1, 2, 3, 4>
Console.WriteLine(processorRegisters.EvaluateXmm(Iced.Intel.Register.XMM3)); // <4, 3, 2, 1>

// Set registers based on the Iced.Intel.Register enumeration
processorRegisters.SetXmm(Iced.Intel.Register.XMM8, Vector128.Create(0F, 8F, 16F, 24F));
Console.WriteLine(processorRegisters.EvaluateXmm(Iced.Intel.Register.XMM8)); // <0, 8, 16, 24>
```
