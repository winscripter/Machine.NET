using Machine.Utility;

namespace Machine.X64.Runtime.Core;

/// <summary>
/// The MXCSR register.
/// </summary>
public struct Mxcsr
{
    private const ulong DefaultValue = 0x1f80;

    private ulong m_value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mxcsr"/> structure.
    /// </summary>
    public Mxcsr()
    {
        m_value = DefaultValue;
    }

    /// <summary>
    /// The 64-bit value of the MXCSR.
    /// </summary>
    public ulong Value
    {
        readonly get => m_value;
        set => m_value = value;
    }

    /// <summary>
    /// Represents the IE (Invalid Operation) exception status.
    /// </summary>
    public bool ExceptionStatusIE
    {
        readonly get => BitUtilities.IsBitSet(m_value, 0);
        set => BitUtilities.SetBit(ref m_value, 0, value);
    }

    /// <summary>
    /// Represents the DE (Denormal) exception status.
    /// </summary>
    public bool ExceptionStatusDE
    {
        readonly get => BitUtilities.IsBitSet(m_value, 1);
        set => BitUtilities.SetBit(ref m_value, 1, value);
    }

    /// <summary>
    /// Represents the ZE (Divide by zero) exception status.
    /// </summary>
    public bool ExceptionStatusZE
    {
        readonly get => BitUtilities.IsBitSet(m_value, 2);
        set => BitUtilities.SetBit(ref m_value, 2, value);
    }

    /// <summary>
    /// Represents the OE (Overflow) exception status.
    /// </summary>
    public bool ExceptionStatusOE
    {
        readonly get => BitUtilities.IsBitSet(m_value, 3);
        set => BitUtilities.SetBit(ref m_value, 3, value);
    }

    /// <summary>
    /// Represents the UE (Underflow) exception status.
    /// </summary>
    public bool ExceptionStatusUE
    {
        readonly get => BitUtilities.IsBitSet(m_value, 4);
        set => BitUtilities.SetBit(ref m_value, 4, value);
    }

    /// <summary>
    /// Represents the PE (Precision) exception status.
    /// </summary>
    public bool ExceptionStatusPE
    {
        readonly get => BitUtilities.IsBitSet(m_value, 5);
        set => BitUtilities.SetBit(ref m_value, 5, value);
    }

    /// <summary>
    /// Represents the Denormals Are Zeros (DAZ) exception control mask.
    /// </summary>
    public bool Daz
    {
        readonly get => BitUtilities.IsBitSet(m_value, 6);
        set => BitUtilities.SetBit(ref m_value, 6, value);
    }

    /// <summary>
    /// Represents the Invalid operation mask (IM) exception control mask.
    /// </summary>
    public bool Im
    {
        readonly get => BitUtilities.IsBitSet(m_value, 7);
        set => BitUtilities.SetBit(ref m_value, 7, value);
    }

    /// <summary>
    /// Represents the Denormal mask (DM) exception control mask.
    /// </summary>
    public bool Dm
    {
        readonly get => BitUtilities.IsBitSet(m_value, 8);
        set => BitUtilities.SetBit(ref m_value, 8, value);
    }

    /// <summary>
    /// Represents the Divide by Zero mask (ZM) exception control mask.
    /// </summary>
    public bool Zm
    {
        readonly get => BitUtilities.IsBitSet(m_value, 9);
        set => BitUtilities.SetBit(ref m_value, 9, value);
    }

    /// <summary>
    /// Represents the Overflow mask (OM) exception control mask.
    /// </summary>
    public bool Om
    {
        readonly get => BitUtilities.IsBitSet(m_value, 10);
        set => BitUtilities.SetBit(ref m_value, 10, value);
    }

    /// <summary>
    /// Represents the Underflow mask (UM) exception control mask.
    /// </summary>
    public bool Um
    {
        readonly get => BitUtilities.IsBitSet(m_value, 11);
        set => BitUtilities.SetBit(ref m_value, 11, value);
    }

    /// <summary>
    /// Represents the Precision mask (PM) exception control mask.
    /// </summary>
    public bool Pm
    {
        readonly get => BitUtilities.IsBitSet(m_value, 12);
        set => BitUtilities.SetBit(ref m_value, 12, value);
    }

    /// <summary>
    /// Represents the Rounding control (RC) exception control mask.
    /// </summary>
    public MxcsrRoundingControl Rc
    {
        readonly get
        {
            bool bit13 = BitUtilities.IsBitSet(m_value, 13);
            bool bit14 = BitUtilities.IsBitSet(m_value, 14);
            return (MxcsrRoundingControl)(bit13 ? 1 : 0 + (bit14 ? 2 : 0));
        }

        set
        {
            BitUtilities.SetBit(ref m_value, 13, BitUtilities.IsBitSet((byte)value, 0));
            BitUtilities.SetBit(ref m_value, 14, BitUtilities.IsBitSet((byte)value, 1));
        }
    }

    /// <summary>
    /// Represents the Flush to zero mode (FZ) exception control mask.
    /// </summary>
    public bool Fz
    {
        readonly get => BitUtilities.IsBitSet(m_value, 15);
        set => BitUtilities.SetBit(ref m_value, 15, value);
    }
}
