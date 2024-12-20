using Iced.Intel;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{
    /// <summary>
    /// Runs the given instruction on the CPU. The CPU is software-based and does not have any hardware dependencies.
    /// Note that this may cause deadlocks in UI based environments due to lack of async support, so
    /// you may want to sometimes invoke <see cref="Task.Yield"/> method to prevent this.
    /// </summary>
    /// <param name="instruction">The instruction.</param>
    public void Run(in Instruction instruction)
    {
        this.CurrentRip = this.ProcessorRegisters.Rip;
        this.LastOrExecutingInstruction = instruction;

        if (instruction.HasRepPrefix)
        {
            for (ulong i = 0; i < this.ProcessorRegisters.Rcx; i++)
            {
                RunCore(in instruction);
            }
        }
        else if (instruction.HasRepePrefix)
        {
            while (this.ProcessorRegisters.RFlagsZF)
            {
                RunCore(in instruction);
            }
        }
        else if (instruction.HasRepnePrefix)
        {
            while (!this.ProcessorRegisters.RFlagsZF)
            {
                RunCore(in instruction);
            }
        }
        else
        {
            RunCore(in instruction);
        }
    }
}
