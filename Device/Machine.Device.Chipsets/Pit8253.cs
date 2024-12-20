namespace Machine.Device.Chipsets;

/// <summary>
/// The Intel 8253 (or i8253) Programmable Interval Timer (PIT) chip was developed by Intel in the late 1970s.
/// </summary>
public class Pit8253
{
    private readonly Counter[] counters;

    public Pit8253()
    {
        counters = new Counter[3];
        for (int i = 0; i < 3; i++)
        {
            counters[i] = new Counter();
        }
    }

    public void WriteControlWord(int counterIndex, int mode)
    {
        if (counterIndex >= 0 && counterIndex < 3)
        {
            counters[counterIndex].Mode = mode;
        }
    }

    public void LoadCounter(int counterIndex, int count)
    {
        if (counterIndex >= 0 && counterIndex < 3)
        {
            counters[counterIndex].LoadCount(count);
        }
    }

    public void SetGate(int counterIndex, bool gate)
    {
        if (counterIndex >= 0 && counterIndex < 3)
        {
            counters[counterIndex].Gate = gate;
        }
    }

    public bool GetOutput(int counterIndex)
    {
        if (counterIndex >= 0 && counterIndex < 3)
        {
            return counters[counterIndex].Output;
        }
        return false;
    }

    public void ClockTick(int counterIndex)
    {
        if (counterIndex >= 0 && counterIndex < 3)
        {
            counters[counterIndex].ClockTick();
        }
    }
}
