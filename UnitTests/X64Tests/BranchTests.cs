using Machine.X64.Runtime;

namespace X64Tests;

public sealed class BranchTests
{
    [Fact]
    public void DoBranchTest1()
    {
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
    }
}
