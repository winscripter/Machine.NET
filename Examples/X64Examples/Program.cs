using Iced.Intel;
using Machine.X64.Runtime;
using static Iced.Intel.AssemblerRegisters;

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
    //throw new InvalidOperationException(cpu.LastOrExecutingInstruction.Code.ToString());
    throw;
}

if(x == 42uL)
    Console.WriteLine("Yes");

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

    private static List<Instruction> Decompile(Assembler assembler)
    {
        var stream = new MemoryStream();
        var streamCodeWriter = new StreamCodeWriter(stream);

        assembler.Assemble(streamCodeWriter, rip: 0uL);

        return Decompile(stream);
    }

    private static List<Instruction> Decompile(MemoryStream stream)
    {
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

        return instrs;
    }
}
