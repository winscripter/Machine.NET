namespace Machine.Device.Chipsets;

/// <summary>
/// Intel 8259 Programmable Interrupt Controller (PIC) is a device used to combine multiple interrupt inputs into a single interrupt output.
/// </summary>
public sealed class Pic8259
{
    /// <summary>
    /// The Interrupt Mask Register (IMR) is a register in the PIC that is used to mask interrupts.
    /// </summary>
    public byte Imr { get; set; }

    /// <summary>
    /// The Interrupt Request Register (IRR) is a register in the PIC that is used to store the interrupt requests.
    /// </summary>
    public byte Irr { get; set; }

    /// <summary>
    /// The Interrupt Service Register (ISR) is a register in the PIC that is used to store the interrupt service requests.
    /// </summary>
    public byte Isr { get; set; }

    /// <summary>
    /// The Interrupt Control Word (ICW) is a set of commands that are used to initialize the PIC.
    /// </summary>
    public byte[] Icw { get; set; } = new byte[4];

    /// <summary>
    /// The Interrupt Control Word Step (ICW1, ICW2, ICW3, ICW4) is a byte that is used to determine the step of the ICW.
    /// </summary>
    public byte IcwStep { get; set; }

    private Pic8259()
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="Pic8259"/> class.
    /// </summary>
    /// <returns>An Intel 8259 Programmable Interrupt Controller.</returns>
    public static Pic8259 Create()
    {
        return new();
    }

    /// <summary>
    /// Checks whether the specified port is a PIC port.
    /// </summary>
    /// <param name="port">The port number</param>
    /// <returns>
    /// A boolean that indicates whether or not <paramref name="port"/> is equal to one of the preceding values:
    /// <br/>
    /// <code>0x20 0x21 0xA0 0xA1</code>
    /// </returns>
    public static bool Listening(ushort port) => port is 0x20 or 0x21 or 0xA0 or 0xA1;

    /// <summary>
    /// Returns and computes the next interrupt.
    /// </summary>
    /// <returns>The interrupt</returns>
    public byte NextInterrupt()
    {
        byte mask = (byte)(Irr & ~Imr);

        for (int i = 0; i < 8; i++)
        {
            if ((mask >> i & 1) >= 1)
            {
                Irr = (byte)(Irr ^ 1 << i);
                Isr = (byte)(Isr | 1 << i);
                return (byte)(Icw[1] + i);
            }
        }

        return 0;
    }

    /// <summary>
    /// Reads the IRR or IMR from the specified port.
    /// </summary>
    /// <param name="port">The port.</param>
    /// <returns>An IRR, IMR, or 0 if the port is invalid.</returns>
    public byte Read(ushort port)
    {
        return port switch
        {
            0x20 or 0xA0 => Irr,
            0x21 or 0xA1 => Imr,
            _ => 0,
        };
    }

    /// <summary>
    /// Writes the data to the specified port.
    /// </summary>
    /// <param name="port">The port to write data to.</param>
    /// <param name="data">The data to write.</param>
    public void WriteData(ushort port, byte data)
    {
        switch (port)
        {
            case 0x20:
            case 0xA0:
                SetPicCommand(data);
                break;

            case 0x21:
            case 0xA1:
                SetPicData(data);
                break;
        }
    }

    /// <summary>
    /// Alters the interrupt.
    /// </summary>
    /// <param name="irq">IRQ</param>
    public void SetInterrupt(byte irq)
    {
        Irr |= (byte)(1 << irq);
    }

    private void SetPicCommand(byte data)
    {
        if ((byte)(data & 0x10) == 0x10)
        {
            Imr = 0;
            Icw[IcwStep++] = data;
        }

        if ((byte)(data & 0x20) == 0x20)
        {
            for (int i = 0; i < 8; i++)
            {
                if ((byte)(Isr >> i & 1) == 1)
                {
                    Isr = (byte)(Isr ^ 1 << i);
                }
            }
        }
    }

    private void SetPicData(byte data)
    {
        if (IcwStep == 1)
        {
            Icw[IcwStep++] = data;
            if ((byte)(Icw[0] & 2) == 0x02)
            {
                IcwStep++;
            }
        }
        else if (IcwStep < 4)
        {
            Icw[IcwStep++] = data;
        }
        else
        {
            Imr = data;
        }
    }
}
