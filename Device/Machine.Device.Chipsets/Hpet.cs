namespace Machine.Device.Chipsets;

/// <summary>
/// Represents the High Precision Event Timer (HPET) which is a hardware timer used in personal computers.
/// </summary>
public class Hpet // Don't mark as sealed (the OnTick method is virtual)
{
    private readonly Counter _counter;
    private readonly Timer _timer;
    private readonly int _frequency;
    private bool _isRunning;

    /// <summary>
    /// Initializes a new instance of the <see cref="Hpet"/> class.
    /// </summary>
    /// <param name="frequency">Frequency</param>
    public Hpet(int frequency)
    {
        _frequency = frequency;
        _counter = new Counter();
        _timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    /// <summary>
    /// Starts the HPET timer.
    /// </summary>
    public void Start()
    {
        _isRunning = true;
        _counter.LoadCount(_frequency);
        _timer.Change(0, 1000 / _frequency);
    }

    /// <summary>
    /// Stops the HPET timer.
    /// </summary>
    public void Stop()
    {
        _isRunning = false;
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
    }

    private void TimerCallback(object? state)
    {
        if (_isRunning)
        {
            _counter.ClockTick();
            if (_counter.Output)
            {
                OnTick();
            }
        }
    }

    /// <summary>
    /// Event that is raised when the HPET timer ticks.
    /// </summary>
    public event Action? Tick;

    public virtual void OnTick()
    {
        Tick?.Invoke();
    }
}
