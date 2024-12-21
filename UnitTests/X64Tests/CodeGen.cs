using Iced.Intel;
using static Iced.Intel.AssemblerRegisters;

namespace X64Tests;

internal static class CodeGen
{
    public static List<Instruction> MakeIOPortTestCode_1()
    {
        var assembler = new Assembler(64);
        assembler.mov(rax, 1);
        assembler.@out(cx, al);

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

        return instrs;
    }
}
