namespace Machine.Device.Chipsets;

public sealed class Counter
{
    public int Count { get; set; }
    public int InitialCount { get; set; }
    public bool Gate { get; set; }
    public bool Output { get; set; }
    public int Mode { get; set; }

    public Counter()
    {
        Count = 0;
        InitialCount = 0;
        Gate = false;
        Output = false;
        Mode = 0;
    }

    public void LoadCount(int count)
    {
        InitialCount = count;
        Count = count;
    }

    public void ClockTick()
    {
        if (Gate)
        {
            Count--;
            if (Count <= 0)
            {
                Count = InitialCount;
                Output = true;
            }
            else
            {
                Output = false;
            }
        }
    }
}
