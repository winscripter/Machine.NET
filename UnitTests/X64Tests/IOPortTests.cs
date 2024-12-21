using Iced.Intel;
using Machine.X64.Runtime;

namespace X64Tests;

public sealed class IOPortTests
{
    [Fact]
    public void TestIOPorts()
    {
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
        var code = CodeGen.MakeIOPortTestCode_1();
        foreach (Instruction instruction in code)
        {
            cpu.Run(in instruction);
        }

        Assert.Equal(7777uL, x);
        Assert.Equal(1234uL, cpu.ProcessorRegisters.Eax);
    }
}
