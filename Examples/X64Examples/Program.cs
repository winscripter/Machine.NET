// Taken from:
// https://github.com/amensch/e8086/blob/master/ConsoleCPUTest/Program.cs

using Machine.X64.Runtime;
using System.Text;

byte[] codeGolf = File.ReadAllBytes("Codegolf.bin");
var rt = new CpuRuntime(memorySize: 65536, ioPortCount: 8, bitness: 16);
rt.LoadProgram(codeGolf, 0uL);
rt.ProcessorRegisters.Cs = 0;
rt.ProcessorRegisters.Rip = 0uL;
rt.SetRsp(0x0100uL);
rt.Use8086Compatibility();

int cycles = 0;
bool completedSuccessfully = false;
rt.RunUntilNotBusyOrTrue(() =>
{
    cycles++;
    if (cycles > 1048576)
    {
        return true;
    }
    bool correctIP = rt.ProcessorRegisters.Ip is not 0x0006 and not 0x0143;
    completedSuccessfully = !correctIP;
    return !correctIP;
});

ushort baseAddress = 0x8000;
var scn = new StringBuilder(2048);

for (ushort row = 0; row < 25; row++)
{
    for (ushort col = 0; col < 80; col++)
    {
        char ch = (char)rt.Memory[baseAddress++];
        if (ch == '\0')
        {
            scn.Append(' ');
        }
        else
        {
            scn.Append(ch);
        }
    }
    scn.AppendLine();
}
Console.Clear();
Console.Write(scn.ToString());
