# Machine.X64.Components.Registers
This simulates CPU registers and flags in pure managed C#.

This package features two types, all under the namespace Machine.X64.Component:

- `IProcessorRegisters` - an abstraction for x64 registers
- `ProcessorRegisters` - x64 registers

Example:

```cs
using Machine.X64.Component;
using System.Runtime.Intrinsics;

var processorRegisters = new ProcessorRegisters();
processorRegisters.Rax = 123;
processorRegisters.Xmm5 = Vector128.Create(1F, 2F, 3F, 4F);
```

Accessing and setting flags is also possible:
```
if (processorRegisters.RFlagsZF) // Zero Flag
{
    processorRegisters.RFlagsVM = true; // Virtual 8086 mode
}

// CR0 and CR4 flags are also supported
processorRegisters.CR0NW = true; // Not Write Through (CR0, at bit 29)
processorRegisters.CR4SMAP = true; // Supervisor Mode Access Prevention (CR4, bit 21)
```
