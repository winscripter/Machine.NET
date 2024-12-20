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

var runtime = new CpuRuntime(memorySize: 65536, ioPortCount: 32); // 64KB RAM
foreach (var instr in instrs)
{
    runtime.Run(in instr);
}

Console.WriteLine(runtime.ProcessorRegisters.Rax);