// Some code was copied from here:
// https://github.com/gregdivis/Aeon/blob/master/src/Aeon.Emulator/Processor/FPU.cs

using Iced.Intel;
using System.Net.NetworkInformation;

namespace Machine.X64.Component;

/// <summary>
/// The floating-point unit.
/// </summary>
public sealed class Fpu
{
    private double[] _st;
    private byte[] _used;
    private long _ptr;

    /// <summary>
    /// Initializes a new instance of the <see cref="Fpu"/> class.
    /// </summary>
    public Fpu()
    {
        _st = new double[8];
        _used = new byte[8];
        _ptr = 0;
        StatusFlags = FpuStatus.Clear;
        ControlWord = 0x3BF;
    }

    /// <summary>
    /// Returns all ST registers, skipping unused ones.
    /// </summary>
    /// <returns>All ST registers.</returns>
    public IEnumerable<double> GetAllSTs()
    {
        return GetAllSTs(zeroOutUnusedOnes: true);
    }

    /// <summary>
    /// Returns all ST registers.
    /// </summary>
    /// <param name="zeroOutUnusedOnes">Should unused ST registers be replaced by zeros?</param>
    /// <returns>All ST registers.</returns>
    public IEnumerable<double> GetAllSTs(bool zeroOutUnusedOnes)
    {
        var result = new List<double>();
        for (int i = 0; i < 8; i++)
        {
            if (!(zeroOutUnusedOnes && _used[i] != 0))
            {
                result.Add(_st[i]);
            }
            else
            {
                result.Add(0D);
            }
        }
        return result;
    }

    /// <summary>
    /// Gets ST0.
    /// </summary>
    public double ST0
    {
        get => _st[0];
    }

    /// <summary>
    /// Gets masked FPU exceptions.
    /// </summary>
    public ExceptionMask MaskedExceptions { get; set; }
    
    /// <summary>
    /// Gets the rounding mode of the FPU.
    /// </summary>
    public RoundingControl RoundingMode { get; set; }
    
    /// <summary>
    /// Gets the precision mode of the FPU.
    /// </summary>
    public PrecisionControl PrecisionMode { get; set; }
    
    /// <summary>
    /// Gets a value indicating whether the interrupt enable mask is set.
    /// </summary>
    public bool InterruptEnableMask { get; set; }
    
    /// <summary>
    /// Gets a value indicating whether the infinity control bit is set.
    /// </summary>
    public bool InfinityControl { get; set; }

    /// <summary>
    /// The FPU status.
    /// </summary>
    public FpuStatus StatusFlags { get; set; }

    /// <summary>
    /// Writes to the ST register.
    /// </summary>
    /// <param name="register">Register.</param>
    /// <param name="value">Value.</param>
    /// <exception cref="ArgumentException"></exception>
    public void SetST(Register register, double value)
    {
        if (register < Register.ST0 || register > Register.ST7)
            throw new ArgumentException("Given register does not match any ST register", nameof(register));

        _st[register - Register.ST0] = value;
    }

    /// <summary>
    /// Returns the ST register.
    /// </summary>
    /// <param name="register">Register.</param>
    /// <remarks>
    /// Value of the given ST register.
    /// </remarks>
    public double GetST(Register register)
    {
        if (register < Register.ST0 || register > Register.ST7)
            throw new ArgumentException("Given register does not match any ST register", nameof(register));

        return _st[register - Register.ST0];
    }

    public ushort ControlWord
    {
        get
        {
            uint value = (uint)MaskedExceptions | ((uint)PrecisionMode << 8) | ((uint)RoundingMode << 10);
            if (InterruptEnableMask)
                value |= 1u << 7;
            if (InfinityControl)
                value |= 1u << 12;

            return (ushort)value;
        }
        set
        {
            MaskedExceptions = (ExceptionMask)(value & 0x3Fu);
            PrecisionMode = (PrecisionControl)((value >> 8) & 0x3u);
            RoundingMode = (RoundingControl)((value >> 10) & 0x3u);
            InterruptEnableMask = (value & (1u << 7)) != 0;
            InfinityControl = (value & (1u << 12)) != 0;
        }
    }

    public ushort TagWord
    {
        get
        {
            unsafe
            {
                uint tag = 0;

                for (int i = 0; i < 8; i++)
                {
                    uint currentValue = 3;
                    if (this._used[i] != 0)
                    {
                        if (this._st[i] == 0)
                            currentValue = 1;
                        else if (double.IsNaN(this._st[i]) || double.IsInfinity(this._st[i]))
                            currentValue = 2;
                        else
                            currentValue = 0;
                    }

                    tag |= currentValue << (i * 2);
                }

                return (ushort)tag;
            }
        }
        set
        {
            unsafe
            {
                for (int i = 0; i < 8; i++)
                {
                    uint currentValue = (uint)(value >> (i * 2)) & 3;
                    this._used[i] = (currentValue != 3) ? (byte)1 : (byte)0;
                }
            }
        }
    }

    public ushort StatusWord
    {
        get
        {
            uint value = (uint)this.StatusFlags | ((uint)_ptr << 11);
            return (ushort)value;
        }
        set
        {
            this.StatusFlags = (FpuStatus)(value & 0xC7FF);
            this._ptr = (uint)((value >> 11) & 7);
        }
    }

    public void Reset()
    {
        _st = new double[8];
        _used = new byte[8];
        StatusFlags = FpuStatus.Clear;
        ControlWord = 0x3BF;
    }

    public double Round(double value)
    {
        var mode = RoundingMode;
        if (mode == RoundingControl.Truncate)
            return Math.Truncate(value);
        else if (mode == RoundingControl.Nearest)
            return Math.Round(value);
        else if (mode == RoundingControl.Down)
            return Math.Floor(value);
        else
            return Math.Ceiling(value);
    }

    public void Pop()
    {
        if (_used[_ptr] != 0)
        {
            _used[_ptr] = 0;
            _ptr = (_ptr + 1u) & 0x7u;
        }
    }

    public void Push(double value)
    {
        _ptr = (_ptr - 1u) & 0x7u;

        if (_used[_ptr] == 0)
        {
            _st[_ptr] = value;
            _used[_ptr] = 1;
        }
    }
}
