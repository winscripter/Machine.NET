using Iced.Intel;

namespace Machine.X64.Component;

/// <summary>
/// Extensions for <see cref="ProcessorRegisters"/>.
/// </summary>
public static partial class RegisterExtensions
{
    /// <summary>
    /// Writes to a 16-bit general-purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to write to (assuming that it's 16-bit).</param>
    /// <param name="value">The value that will be set.</param>
    /// <remarks>
    /// If <paramref name="register"/> contains a register which does not match any
    /// 16-bit general-purpose or segment register, nothing will happen.
    /// </remarks>
    public static void WriteToRegister16(this ProcessorRegisters registers, Register register, ushort value)
    {
        switch (register)
        {
            case Register.AX: registers.Ax = value; break;
            case Register.BX: registers.Bx = value; break;
            case Register.CX: registers.Cx = value; break;
            case Register.DX: registers.Dx = value; break;
            case Register.CS: registers.Cs = value; break;
            case Register.DS: registers.Ds = value; break;
            case Register.SS: registers.Ss = value; break;
            case Register.ES: registers.Es = value; break;
            case Register.BP: registers.Bp = value; break;
            case Register.SI: registers.Si = value; break;
            case Register.SP: registers.Sp = value; break;
            case Register.DI: registers.Di = value; break;
        }
    }

    /// <summary>
    /// Writes to a 32-bit general-purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to write to (assuming that it's 32-bit).</param>
    /// <param name="value">The value that will be set.</param>
    /// <remarks>
    /// If <paramref name="register"/> contains a register which does not match any
    /// 32-bit general-purpose or segment register, nothing will happen.
    /// </remarks>
    public static void WriteToRegister32(this ProcessorRegisters registers, Register register, uint value)
    {
        switch (register)
        {
            case Register.EAX: registers.Eax = value; break;
            case Register.EBX: registers.Ebx = value; break;
            case Register.ECX: registers.Ecx = value; break;
            case Register.EDX: registers.Edx = value; break;
            case Register.EBP: registers.Ebp = value; break;
            case Register.ESI: registers.Esi = value; break;
            case Register.ESP: registers.Esp = value; break;
            case Register.EDI: registers.Edi = value; break;
        }
    }

    /// <summary>
    /// Writes to a 64-bit general-purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to write to (assuming that it's 64-bit).</param>
    /// <param name="value">The value that will be set.</param>
    /// <remarks>
    /// If <paramref name="register"/> contains a register which does not match any
    /// 64-bit general-purpose or segment register, nothing will happen.
    /// </remarks>
    public static void WriteToRegister64(this ProcessorRegisters registers, Register register, ulong value)
    {
        switch (register)
        {
            case Register.RAX: registers.Rax = value; break;
            case Register.RBX: registers.Rbx = value; break;
            case Register.RCX: registers.Rcx = value; break;
            case Register.RDX: registers.Rdx = value; break;
            case Register.RBP: registers.Rbp = value; break;
            case Register.RSI: registers.Rsi = value; break;
            case Register.RSP: registers.Rsp = value; break;
            case Register.RDI: registers.Rdi = value; break;
            case Register.GS: registers.Gs = value; break;
            case Register.FS: registers.Fs = value; break;
        }
    }

    /// <summary>
    /// Writes to an 8-bit general-purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to write to (assuming that it's 8-bit).</param>
    /// <param name="value">The value that will be set.</param>
    /// <remarks>
    /// If <paramref name="register"/> contains a register which does not match any
    /// 8-bit general-purpose or segment register, nothing will happen.
    /// </remarks>
    public static void WriteToRegister8(this ProcessorRegisters registers, Register register, byte value)
    {
        switch (register)
        {
            case Register.AL: registers.Al = value; break;
            case Register.AH: registers.Ah = value; break;
            case Register.BL: registers.Bh = value; break;
            case Register.BH: registers.Bh = value; break;
            case Register.CL: registers.Cl = value; break;
            case Register.CH: registers.Ch = value; break;
            case Register.DL: registers.Dl = value; break;
            case Register.DH: registers.Dh = value; break;
        }
    }

    /// <summary>
    /// Evaluates &amp; returns the value of an 8-bit general purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to alter (assuming that it's 8-bit).</param>
    /// <remarks>
    /// This returns 0 if <paramref name="register"/> does not match any 8-bit general purpose
    /// or segment register.
    /// </remarks>
    /// <returns>Value of an 8-bit general-purpose or segment register specified by the <paramref name="register"/> parameter.</returns>
    public static byte EvaluateRegisterValue8(this ProcessorRegisters registers, Register register)
    {
        return register switch
        {
            Register.AL => registers.Al,
            Register.AH => registers.Ah,

            Register.BL => registers.Bl,
            Register.BH => registers.Bh,

            Register.CL => registers.Cl,
            Register.CH => registers.Ch,

            Register.DL => registers.Dl,
            Register.DH => registers.Dh,

            _ => 0
        };
    }

    /// <summary>
    /// Evaluates &amp; returns the value of an 16-bit general purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to alter (assuming that it's 16-bit).</param>
    /// <remarks>
    /// This returns 0 if <paramref name="register"/> does not match any 16-bit general purpose
    /// or segment register.
    /// </remarks>
    /// <returns>Value of an 16-bit general-purpose or segment register specified by the <paramref name="register"/> parameter.</returns>
    public static ushort EvaluateRegisterValue16(this ProcessorRegisters registers, Register register)
    {
        return register switch
        {
            Register.AX => registers.Ax,
            Register.BX => registers.Bx,
            Register.CX => registers.Cx,
            Register.DX => registers.Dx,
            Register.CS => registers.Cs,
            Register.DS => registers.Ds,
            Register.ES => registers.Es,
            Register.SS => registers.Ss,
            Register.BP => registers.Bp,
            Register.SI => registers.Si,
            Register.DI => registers.Di,
            Register.SP => registers.Sp,
            _ => 0
        };
    }

    /// <summary>
    /// Evaluates &amp; returns the value of an 64-bit general purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to alter (assuming that it's 64-bit).</param>
    /// <remarks>
    /// This returns 0 if <paramref name="register"/> does not match any 64-bit general purpose
    /// or segment register.
    /// </remarks>
    /// <returns>Value of an 64-bit general-purpose or segment register specified by the <paramref name="register"/> parameter.</returns>
    public static ulong EvaluateRegisterValue64(this ProcessorRegisters registers, Register register)
    {
        return register switch
        {
            Register.RAX => registers.Rax,
            Register.RBX => registers.Rbx,
            Register.RCX => registers.Rcx,
            Register.RDX => registers.Rdx,
            Register.RBP => registers.Rbp,
            Register.RSI => registers.Rsi,
            Register.RDI => registers.Rdi,
            Register.RSP => registers.Rsp,
            Register.RIP => registers.Rip,
            Register.GS => registers.Gs,
            Register.FS => registers.Fs,
            _ => 0
        };
    }

    /// <summary>
    /// Evaluates &amp; returns the value of an 32-bit general purpose or segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to alter (assuming that it's 32-bit).</param>
    /// <remarks>
    /// This returns 0 if <paramref name="register"/> does not match any 32-bit general purpose
    /// or segment register.
    /// </remarks>
    /// <returns>Value of an 32-bit general-purpose or segment register specified by the <paramref name="register"/> parameter.</returns>
    public static uint EvaluateRegisterValue32(this ProcessorRegisters registers, Register register)
    {
        return register switch
        {
            Register.EAX => registers.Eax,
            Register.EBX => registers.Ebx,
            Register.ECX => registers.Ecx,
            Register.EDX => registers.Edx,
            Register.EBP => registers.Ebp,
            Register.ESI => registers.Esi,
            Register.EDI => registers.Edi,
            Register.ESP => registers.Esp,
            Register.EIP => registers.Eip,
            _ => 0
        };
    }

    /// <summary>
    /// Updates the lower 8 bits of the FLAGS register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="value">New value.</param>
    public static void UpdateLowerEightBitsOfFlagsRegister(this ProcessorRegisters registers, byte value)
    {
        registers.Flags = (byte)((registers.Flags & 0xFF00) | value);
    }

    /// <summary>
    /// Evaluates &amp; returns the value of an 32-bit segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to return (assuming that it's 64-bit).</param>
    /// <remarks>
    /// This returns 0 if <paramref name="register"/> does not match any 64-bit
    /// segment register.
    /// </remarks>
    /// <returns>Value of a 32-bit gsegment register specified by the <paramref name="register"/> parameter.</returns>
    public static ulong EvaluateSReg(this ProcessorRegisters registers, Register register)
    {
        return register switch
        {
            Register.CS => registers.Cs,
            Register.SS => registers.Ss,
            Register.DS => registers.Ds,
            Register.ES => registers.Es,
            Register.FS => registers.Fs,
            Register.GS => registers.Gs,
            _ => 0uL
        };
    }

    /// <summary>
    /// Writes to 64-bit or 16-bit segment register.
    /// </summary>
    /// <param name="registers">All registers.</param>
    /// <param name="register">Register to write to (assuming that it's 16/64 bit).</param>
    /// <param name="value">The value that will be set.</param>
    /// <remarks>
    /// If <paramref name="register"/> contains a register which does not match any
    /// 64 or 16 bit segment register, nothing will happen.
    /// </remarks>
    public static void SetSReg(this ProcessorRegisters registers, Register register, ulong value)
    {
        switch (register)
        {
            case Register.CS: registers.Cs = (ushort)value; break;
            case Register.DS: registers.Ds = (ushort)value; break;
            case Register.SS: registers.Ss = (ushort)value; break;
            case Register.ES: registers.Es = (ushort)value; break;
            case Register.FS: registers.Fs = value; break;
            case Register.GS: registers.Gs = value; break;
        }
    }

    /// <summary>
    /// Returns the CPL (Current Privilege Level) from the Code Segment register.
    /// This is the lowest 2 bits of the CS register.
    /// </summary>
    /// <remarks>
    ///   This concept was introduced in the Intel 80286 processor (shortly referred
    ///   to as i286 or just 286). Earlier processors (8086, 80186, 8087, 80187)
    ///   do not define the CPL and the CS register is used as a relative offset
    ///   for the IP register to locate the current code.
    /// </remarks>
    /// <param name="registers">Statistics of the CPU register.</param>
    /// <returns>
    /// The CPL (2 bits). Commonly used to define which instructions are and
    /// aren't allowed to avoid risk of OS or hardware corruption.
    /// <list type="bullet">
    ///   <item>
    ///       <para>0 - Kernel (The CPU can run almost everything).</para>
    ///   </item>
    ///   <item>
    ///       <para>1 - Device Drivers (Major amount of low level system
    ///       functions can be run).</para>
    ///   </item>
    ///   <item>
    ///       <para>2 - Device Drivers (Less privileged than 1 but
    ///       still, some low level system functions can run).</para>
    ///   </item>
    ///   <item>
    ///       <para>3 - Applications (Some instructions won't run to
    ///       ensure that a single application doesn't cause damage
    ///       to the OS or the computer).</para>
    ///   </item>
    /// </list>
    /// </returns>
    public static byte GetCpl(this ProcessorRegisters registers)
    {
        return (byte)(registers.Cs & 3);
    }
}
