#if SUPPORT_CPUID

namespace Machine.X64.Runtime;

/// <summary>
/// Customization of the CPUID instruction.
/// </summary>
internal sealed class CpuidProvider
{
    private byte[] _manufacturerName = [(byte)'.', (byte)'N', (byte)'E', (byte)'T', 0, 0, 0, 0, 0, 0, 0, 0];
    /// <summary>
    /// Specifies the CPU manufacturer name. This is used by the CPUID
    /// instruction while EAX is 0. Default value is .NET followed by eight NULL bytes.
    /// </summary>
    public byte[] ManufacturerName
    {
        get => _manufacturerName;
        set
        {
            if (value.Length != 12)
            {
                throw new ArgumentException("Length of manufacturer name must be 12", nameof(value));
            }

            _manufacturerName = value;
        }
    }

    /// <summary>
    /// Value moved into EAX by this code:
    /// <code>
    ///   mov eax, 1
    ///   cpuid
    /// </code>
    /// </summary>
    /// <remarks>
    ///   Bitness of the result:
    ///   <list type="bullet">
    ///     <item>
    ///       <para>31-28: Reserved</para>
    ///     </item>
    ///     <item>
    ///       <para>27-20: Extended Family ID</para>
    ///     </item>
    ///     <item>
    ///       <para>19-16: Extended Model ID</para>
    ///     </item>
    ///     <item>
    ///       <para>15-14: Reserved</para>
    ///     </item>
    ///     <item>
    ///       <para>13-12: Processor Type</para>
    ///     </item>
    ///     <item>
    ///       <para>11-8: Family ID</para>
    ///     </item>
    ///     <item>
    ///       <para>7-4: Model</para>
    ///     </item>
    ///     <item>
    ///       <para>3-0: Stepping ID</para>
    ///     </item>
    ///   </list>
    /// <para />
    /// <para><b>Processor Type</b></para>
    /// <para>
    ///   <list type="bullet">
    ///     <item>00 - OEM processor</item>
    ///     <item>01 - Intel Pentium Overdrive CPU</item>
    ///     <item>10 - Dual processor (applies to Intel P5 only)</item>
    ///     <item>11 - Reserved</item>
    ///   </list>
    /// </para>
    /// <para><b>Processor Family IDs</b></para>
    /// <para>
    ///   Modern OSes like Windows and Linux use these to classify the supported
    ///   processor.
    ///   <list type="bullet">
    ///     0 - None
    ///   </list>
    ///   <list type="bullet">
    ///     1 - None
    ///   </list>
    ///   <list type="bullet">
    ///     2 - None
    ///   </list>
    ///   <list type="bullet">
    ///     3 - None
    ///   </list>
    ///   <list type="bullet">
    ///     4 - Intel: 486; AMD: 486, 5x86, Élan SC4xx/5xx; Other: Cyrix 5x86, Cyrix MediaGX, UMC Green CPU, MCST Elbrus (most models), and MiSTer ao486
    ///   </list>
    ///   <list type="bullet">
    ///     5 - Intel: Pentium, Pentium MMX, Quark X1000; AMD: K5, K6; Other: Cyrix 6x86, Cyrix MediaGXm, Geode(except NX), NexGen Nx586, IDT WinChip, IDT WinChip 2, IDT WinChip 3, Transmeta Crusoe, Rise mP6, SiS 550, DM&amp;P Vortex86 (early), RDC IAD 100, MCST Elbrus-8C2
    ///   </list>
    ///   <list type="bullet">
    ///     6 - Intel: Pentium Pro, Pentium II, Pentium III, Pentium M, Intel Core(all variants), Intel Atom(all variants), Xeon(except NetBurst variants), Xeon Phi(except KNC), AMD: K7 Athlon, Athlon XP; Other: Cyrix 6x86MX/MII, VIA C3, VIA C7, VIA Nano, DM&amp;P Vortex86(DX3, EX2), Zhaoxin ZX-A/B/C/C+, (Centaur CNS), MCST Elbrus-12C/16C/2C3
    ///   </list>
    ///   <list type="bullet">
    ///     7 - Intel: Itanium (in IA-32 mode); Other: Zhaoxin KaiXian, Zhaoxin KaisHeng
    ///   </list>
    ///   <list type="bullet">
    ///     8 - None
    ///   </list>
    ///   <list type="bullet">
    ///     9 - None
    ///   </list>
    ///   <list type="bullet">
    ///     10 - None
    ///   </list>
    ///   <list type="bullet">
    ///     11 - Intel: Xeon Phi (Knights Corner)
    ///   </list>
    ///   <list type="bullet">
    ///     12 - None
    ///   </list>
    ///   <list type="bullet">
    ///     13 - None
    ///   </list>
    ///   <list type="bullet">
    ///     14 - None
    ///   </list>
    ///   <list type="bullet">
    ///     15 - Intel: NetBurst Pentium 4; AMD: K8/Hammer (Athlon 64); Other: Transmeta Efficeon
    ///   </list>
    ///   <list type="bullet">
    ///     16 - AMD: K10 Phenom
    ///   </list>
    ///   <list type="bullet">
    ///     17 - Intel: Itanium (in IA-32 mode); AMD: Turion X2
    ///   </list>
    ///   <list type="bullet">
    ///     18 - AMD: Llano
    ///   </list>
    ///   <list type="bullet">
    ///     19 - Intel: Intel Core (Panther Cove and up)
    ///   </list>
    ///   <list type="bullet">
    ///     20 - AMD: Bobcat
    ///   </list>
    ///   <list type="bullet">
    ///     21 - AMD: Bulldozer, Piledriver, Steamroller, Excavator
    ///   </list>
    ///   <list type="bullet">
    ///     22 - AMD: Jaguar, Puma
    ///   </list>
    ///   <list type="bullet">
    ///     23 - AMD: Zen 1, Zen 2
    ///   </list>
    ///   <list type="bullet">
    ///     24 - AMD/Other: Hygon Dhyana (AMD-Chinese Joint Venture)
    ///   </list>
    ///   <list type="bullet">
    ///     25 - AMD: Zen 3, Zen 4
    ///   </list>
    ///   <list type="bullet">
    ///     26 - Zen 5
    ///   </list>
    /// </para>
    /// </remarks>
    public uint Eax1ValueIntoEax { get; set; }

    /// <summary>
    /// Value moved into EDX by this code:
    /// <code>
    ///   mov eax, 1
    ///   cpuid
    /// </code>
    /// Bits:
    /// <para>0 - FPU (Onboard x87 FPU) - SUPPORTED</para>
    /// <para>1 - VME (Virtual 8086 mode extensions (such as VIF, VIP, PVI)) - SUPPORTED</para>
    /// <para>2 - DE (Debugging extensions (CR4 bit 3)) - SUPPORTED</para>
    /// <para>3 - PSE (Page Size Extension (4 MB pages)) - <b>UNSUPPORTED</b></para>
    /// <para>4 - TSC (Time Stamp Counter and RDTSC instruction) - SUPPORTED</para>
    /// <para>5 - MSR (Model-specific registers and RDMSR/WRMSR instructions) - SUPPORTED</para>
    /// <para>6 - PAE (Physical Address Extensions) - <b>UNSUPPORTED</b></para>
    /// <para>7 - MCE (Machine Check Exception) - SUPPORTED</para>
    /// <para>8 - CX8 (CMPXCHG8B instruction) - SUPPORTED</para>
    /// <para>9 - APIC (Onboard Advanced Programmable Interrupt Controller) - SUPPORTED</para>
    /// <para>10 - MTRR - RESERVED</para>
    /// <para>11 - SEP (SYSENTER &amp; SYSEXIT instructions) - SUPPORTED</para>
    /// <para>12 - MTRR (Memory Type Range Registers) - SUPPORTED</para>
    /// <para>13 - PGE (Page Global Enable in CR4) - <b>UNSUPPORTED</b></para>
    /// <para>14 - MCA (Machine Check Architecture) - <b>UNSUPPORTED</b></para>
    /// <para>15 - CMOV (Conditional move, e.g. CMOVcc, FCMOVcc and FCOMIcc instructions) - SUPPORTED</para>
    /// <para>16 - PAT (Page Attribute Table) - <b>UNSUPPORTED</b></para>
    /// <para>17 - PSE36 (36-bit page size extension) - <b>UNSUPPORTED</b></para>
    /// <para>18 - PSN (Processor Serial Number) - <b>UNSUPPORTED</b></para>
    /// <para>19 - CLFSH (CLFLUSH instruction) - <b>UNSUPPORTED</b></para>
    /// <para>20 - NX (No Execute Bit; Itanium only) - <b>UNSUPPORTED</b></para>
    /// <para>21 - DS (Debug store: save trace of executed jumps) - <b>UNSUPPORTED (for performance reasons)</b></para>
    /// <para>22 - ACPI (Onboard thermal control MSRs for ACPI) - SUPPORTED</para>
    /// <para>23 - MMX (MMX instructions (64-bit SIMD)) - SUPPORTED</para>
    /// <para>24 - FXSR (FXSAVE, FXRSTOR instructions, CR4 bit 9) - <b>UNSUPPORTED</b></para>
    /// <para>25 - SSE (Streaming SIMD Extensions) - <b>Mostly supported</b></para>
    /// <para>26 - SSE2 (Streaming SIMD Extensions 2) - <b>Mostly supported</b></para>
    /// <para>27 - SS (CPU cache implements self-snoop) - <b>UNSUPPORTED</b></para>
    /// <para>28 - HTT (Max APIC IDs reserved field is Valid) - <b>???</b></para>
    /// <para>29 - TM (Thermal monitor automatically limits temperature) - <b>UNSUPPORTED</b></para>
    /// <para>30 - IA64 (IA64 processor emulating x86) - <b>UNSUPPORTED</b></para>
    /// <para>31 - PBE (Pending Break Enable (PBE# pin) wakeup capability) - <b>UNSUPPORTED</b></para>
    /// </summary>
    public uint Eax1ValueIntoEdx { get; set; } = 0b11101101110110010000001101100000;

    /// <summary>
    /// Value moved into EDX by this code:
    /// <code>
    ///   mov eax, 1
    ///   cpuid
    /// </code>
    /// Bits:
    /// <para>0 - SSE3 (Streaming SIMD Extensions 3) - <b>Mostly supported</b></para>
    /// <para>1 - PCMULQDQ (Carry-less multiply instruction) - SUPPORTED</para>
    /// <para>2 - DTES64 (64-bit debug store (edx bit 21)) - <b>UNSUPPORTED</b></para>
    /// <para>3 - MONITOR (MONITOR and MWAIT instructions) - SUPPORTED</para>
    /// <para>4 - DS-CPL (CPL qualified debug store) - <b>UNSUPPORTED</b></para>
    /// <para>5 - VMX (Virtual Machine Extensions) - <b>UNSUPPORTED</b></para>
    /// <para>6 - SMX (Safer Mode Extensions) - <b>UNSUPPORTED</b></para>
    /// <para>7 - EST (Enhanced Speed Step) - <b>UNSUPPORTED</b></para>
    /// <para>8 - TM2 (Thermal Monitor 2) - <b>UNSUPPORTED</b></para>
    /// <para>9 - SSSE3 (Supplemental Streaming SIMD Extensions 3) - SUPPORTED</para>
    /// <para>10 - CNXT (L1 Context ID) - <b>UNSUPPORTED</b></para>
    /// <para>11 - SDBG (Silicon Debug interface) - <b>UNSUPPORTED</b></para>
    /// <para>12 - FMA (Fused Multiply Add; FMA3) - SUPPORTED</para>
    /// <para>13 - CX16 (CMPXCHG16B instruction) - SUPPORTED</para>
    /// <para>14 - XTPR (Can disable sending task priority messages) - <b>UNSUPPORTED</b></para>
    /// <para>15 - PDCM (Perfmon &amp; debug capability) - <b>UNSUPPORTED</b></para>
    /// <para>17 - PCID (Process context identifiers (CR4 bit 17)) - <b>UNSUPPORTED</b></para>
    /// <para>18 - DCA (Direct cache access for DMA writes) - <b>UNSUPPORTED</b></para>
    /// <para>19 - SSE4.1 (Streaming SIMD Extensions 4.1) - SUPPORTED</para>
    /// <para>20 - SSE4.2 (Streaming SIMD Extensions 4.2) - SUPPORTED</para>
    /// <para>21 - X2APIC (x2APIC (enhanced APIC)) - SUPPORTED</para>
    /// <para>22 - MOVBE (MOVBE big endian move instructions) - SUPPORTED</para>
    /// <para>23 - POPCNT (POPCNT instruction) - SUPPORTED</para>
    /// <para>24 - tsc-deadline (APIC implements one-shot operation using a TSC deadline value) - SUPPORTED</para>
    /// <para>25 - AES-NI (AES instruction set) - SUPPORTED</para>
    /// <para>26 - XSAVE (Extensible processor save/restore instructions XSAVE, XRSTOR, XSETBV, XGETBV) - SUPPORTED</para>
    /// <para>27 - OSXSAVE (XSAVE enabled by OS) - SUPPORTED</para>
    /// <para>28 - AVX (Advanced Vector Extensions) - SUPPORTED</para>
    /// <para>29 - F16C (Half precision floating point number conversion instructions) - SUPPORTED</para>
    /// <para>30 - RDRND (RDRAND instruction) - SUPPORTED</para>
    /// <para>31 - HYPERVISOR (Hypervisor present) - <b>UNSUPPORTED</b></para>
    /// </summary>
    public uint Eax1ValueIntoEcx { get; set; } = 0b1101000001001100001111111111110;
}
#endif