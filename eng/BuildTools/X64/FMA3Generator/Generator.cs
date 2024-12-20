using System.Text;

namespace FMA3Generator;

internal sealed class Generator
{
    private string _dir;

    public Generator()
    {
        _dir = Directory.GetCurrentDirectory();
    }

    public Generator(string dir)
    {
        _dir = dir;
    }

    public string OutputDirectory
    {
        get => _dir;
        set => _dir = value;
    }

    public void Vfmaddsub()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfmaddsub132ps");
        Algorithm("(a * c) ? b", "float", "Vfmaddsub132ps", "128", "X");
        Algorithm("(a * c) ? b", "float", "Vfmaddsub132ps", "256", "Y");
        Algorithm("(a * c) ? b", "float", "Vfmaddsub132ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub132ps.g.cs");

        CodeStart(sb, "Vfmaddsub213ps");
        Algorithm("(b * a) ? c", "float", "Vfmaddsub213ps", "128", "X");
        Algorithm("(b * a) ? c", "float", "Vfmaddsub213ps", "256", "Y");
        Algorithm("(b * a) ? c", "float", "Vfmaddsub213ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub213ps.g.cs");

        CodeStart(sb, "Vfmaddsub231ps");
        Algorithm("(c * b) ? a", "float", "Vfmaddsub231ps", "128", "X");
        Algorithm("(c * b) ? a", "float", "Vfmaddsub231ps", "256", "Y");
        Algorithm("(c * b) ? a", "float", "Vfmaddsub231ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub231ps.g.cs");

        // Double
        CodeStart(sb, "Vfmaddsub132pd");
        Algorithm("(a * c) ? b", "double", "Vfmaddsub132pd", "128", "X");
        Algorithm("(a * c) ? b", "double", "Vfmaddsub132pd", "256", "Y");
        Algorithm("(a * c) ? b", "double", "Vfmaddsub132pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub132pd.g.cs");

        CodeStart(sb, "Vfmaddsub213pd");
        Algorithm("(b * a) ? c", "double", "Vfmaddsub213pd", "128", "X");
        Algorithm("(b * a) ? c", "double", "Vfmaddsub213pd", "256", "Y");
        Algorithm("(b * a) ? c", "double", "Vfmaddsub213pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub213pd.g.cs");

        CodeStart(sb, "Vfmaddsub231pd");
        Algorithm("(c * b) ? a", "double", "Vfmaddsub231pd", "128", "X");
        Algorithm("(c * b) ? a", "double", "Vfmaddsub231pd", "256", "Y");
        Algorithm("(c * b) ? a", "double", "Vfmaddsub231pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub231pd.g.cs");

        // Half
        CodeStart(sb, "Vfmaddsub132ph");
        Algorithm("(a * c) ? b", "Half", "Vfmaddsub132ph", "128", "X");
        Algorithm("(a * c) ? b", "Half", "Vfmaddsub132ph", "256", "Y");
        Algorithm("(a * c) ? b", "Half", "Vfmaddsub132ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub132ph.g.cs");

        CodeStart(sb, "Vfmaddsub213ph");
        Algorithm("(b * a) ? c", "Half", "Vfmaddsub213ph", "128", "X");
        Algorithm("(b * a) ? c", "Half", "Vfmaddsub213ph", "256", "Y");
        Algorithm("(b * a) ? c", "Half", "Vfmaddsub213ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub213ph.g.cs");

        CodeStart(sb, "Vfmaddsub231ph");
        Algorithm("(c * b) ? a", "Half", "Vfmaddsub231ph", "128", "X");
        Algorithm("(c * b) ? a", "Half", "Vfmaddsub231ph", "256", "Y");
        Algorithm("(c * b) ? a", "Half", "Vfmaddsub231ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmaddsub231ph.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm128b16",
                    "256" => "ymmm256b16",
                    "512" => "zmmm512b16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm128b32",
                    "256" => "ymmm256b32",
                    "512" => "zmmm512b32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm128b64",
                    "256" => "ymmm256b64",
                    "512" => "zmmm512b64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    for (int i = 0; i < Vector{bitness}<{type}>.Count; i++)
                    {{
                        if (!HasBitSetInK1(i))
                        {{
                            result = result.WithElement(
                                i,
                                i % 2 == 0
                                ? {algorithm.Replace('?', '+')}
                                : {algorithm.Replace('?', '-')});
                        }}
                    }}

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void Vfmsubadd()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfmsubadd132ps");
        Algorithm("(a * c) ? b", "float", "Vfmsubadd132ps", "128", "X");
        Algorithm("(a * c) ? b", "float", "Vfmsubadd132ps", "256", "Y");
        Algorithm("(a * c) ? b", "float", "Vfmsubadd132ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd132ps.g.cs");

        CodeStart(sb, "Vfmsubadd213ps");
        Algorithm("(b * a) ? c", "float", "Vfmsubadd213ps", "128", "X");
        Algorithm("(b * a) ? c", "float", "Vfmsubadd213ps", "256", "Y");
        Algorithm("(b * a) ? c", "float", "Vfmsubadd213ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd213ps.g.cs");

        CodeStart(sb, "Vfmsubadd231ps");
        Algorithm("(c * b) ? a", "float", "Vfmsubadd231ps", "128", "X");
        Algorithm("(c * b) ? a", "float", "Vfmsubadd231ps", "256", "Y");
        Algorithm("(c * b) ? a", "float", "Vfmsubadd231ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd231ps.g.cs");

        // Double
        CodeStart(sb, "Vfmsubadd132pd");
        Algorithm("(a * c) ? b", "double", "Vfmsubadd132pd", "128", "X");
        Algorithm("(a * c) ? b", "double", "Vfmsubadd132pd", "256", "Y");
        Algorithm("(a * c) ? b", "double", "Vfmsubadd132pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd132pd.g.cs");

        CodeStart(sb, "Vfmsubadd213pd");
        Algorithm("(b * a) ? c", "double", "Vfmsubadd213pd", "128", "X");
        Algorithm("(b * a) ? c", "double", "Vfmsubadd213pd", "256", "Y");
        Algorithm("(b * a) ? c", "double", "Vfmsubadd213pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd213pd.g.cs");

        CodeStart(sb, "Vfmsubadd231pd");
        Algorithm("(c * b) ? a", "double", "Vfmsubadd231pd", "128", "X");
        Algorithm("(c * b) ? a", "double", "Vfmsubadd231pd", "256", "Y");
        Algorithm("(c * b) ? a", "double", "Vfmsubadd231pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd231pd.g.cs");

        // Half
        CodeStart(sb, "Vfmsubadd132ph");
        Algorithm("(a * c) ? b", "Half", "Vfmsubadd132ph", "128", "X");
        Algorithm("(a * c) ? b", "Half", "Vfmsubadd132ph", "256", "Y");
        Algorithm("(a * c) ? b", "Half", "Vfmsubadd132ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd132ph.g.cs");

        CodeStart(sb, "Vfmsubadd213ph");
        Algorithm("(b * a) ? c", "Half", "Vfmsubadd213ph", "128", "X");
        Algorithm("(b * a) ? c", "Half", "Vfmsubadd213ph", "256", "Y");
        Algorithm("(b * a) ? c", "Half", "Vfmsubadd213ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd213ph.g.cs");

        CodeStart(sb, "Vfmsubadd231ph");
        Algorithm("(c * b) ? a", "Half", "Vfmsubadd231ph", "128", "X");
        Algorithm("(c * b) ? a", "Half", "Vfmsubadd231ph", "256", "Y");
        Algorithm("(c * b) ? a", "Half", "Vfmsubadd231ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsubadd231ph.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm128b16",
                    "256" => "ymmm256b16",
                    "512" => "zmmm512b16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm128b32",
                    "256" => "ymmm256b32",
                    "512" => "zmmm512b32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm128b64",
                    "256" => "ymmm256b64",
                    "512" => "zmmm512b64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    for (int i = 0; i < Vector{bitness}<{type}>.Count; i++)
                    {{
                        if (!HasBitSetInK1(i))
                        {{
                            result = result.WithElement(
                                i,
                                i % 2 == 0
                                ? {algorithm.Replace('?', '-')}
                                : {algorithm.Replace('?', '+')});
                        }}
                    }}

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    private void ExportFile(StringBuilder sb, string fileName)
    {
        File.AppendAllText(
            Path.Combine(_dir, fileName),
            sb.ToString());

        sb.Clear();
    }

    public void VfmaddPS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfmadd132ps");
        Algorithm("(a * c) ? b", "float", "Vfmadd132ps", "128", "X");
        Algorithm("(a * c) ? b", "float", "Vfmadd132ps", "256", "Y");
        Algorithm("(a * c) ? b", "float", "Vfmadd132ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd132ps.g.cs");

        CodeStart(sb, "Vfmadd213ps");
        Algorithm("(b * a) ? c", "float", "Vfmadd213ps", "128", "X");
        Algorithm("(b * a) ? c", "float", "Vfmadd213ps", "256", "Y");
        Algorithm("(b * a) ? c", "float", "Vfmadd213ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd213ps.g.cs");

        CodeStart(sb, "Vfmadd231ps");
        Algorithm("(c * b) ? a", "float", "Vfmadd231ps", "128", "X");
        Algorithm("(c * b) ? a", "float", "Vfmadd231ps", "256", "Y");
        Algorithm("(c * b) ? a", "float", "Vfmadd231ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd231ps.g.cs");

        // Double
        CodeStart(sb, "Vfmadd132pd");
        Algorithm("(a * c) ? b", "double", "Vfmadd132pd", "128", "X");
        Algorithm("(a * c) ? b", "double", "Vfmadd132pd", "256", "Y");
        Algorithm("(a * c) ? b", "double", "Vfmadd132pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd132pd.g.cs");

        CodeStart(sb, "Vfmadd213pd");
        Algorithm("(b * a) ? c", "double", "Vfmadd213pd", "128", "X");
        Algorithm("(b * a) ? c", "double", "Vfmadd213pd", "256", "Y");
        Algorithm("(b * a) ? c", "double", "Vfmadd213pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd213pd.g.cs");

        CodeStart(sb, "Vfmadd231pd");
        Algorithm("(c * b) ? a", "double", "Vfmadd231pd", "128", "X");
        Algorithm("(c * b) ? a", "double", "Vfmadd231pd", "256", "Y");
        Algorithm("(c * b) ? a", "double", "Vfmadd231pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd231pd.g.cs");

        // Half
        CodeStart(sb, "Vfmadd132ph");
        Algorithm("(a * c) ? b", "Half", "Vfmadd132ph", "128", "X");
        Algorithm("(a * c) ? b", "Half", "Vfmadd132ph", "256", "Y");
        Algorithm("(a * c) ? b", "Half", "Vfmadd132ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd132ph.g.cs");

        CodeStart(sb, "Vfmadd213ph");
        Algorithm("(b * a) ? c", "Half", "Vfmadd213ph", "128", "X");
        Algorithm("(b * a) ? c", "Half", "Vfmadd213ph", "256", "Y");
        Algorithm("(b * a) ? c", "Half", "Vfmadd213ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd213ph.g.cs");

        CodeStart(sb, "Vfmadd231ph");
        Algorithm("(c * b) ? a", "Half", "Vfmadd231ph", "128", "X");
        Algorithm("(c * b) ? a", "Half", "Vfmadd231ph", "256", "Y");
        Algorithm("(c * b) ? a", "Half", "Vfmadd231ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd231ph.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm128b16",
                    "256" => "ymmm256b16",
                    "512" => "zmmm512b16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm128b32",
                    "256" => "ymmm256b32",
                    "512" => "zmmm512b32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm128b64",
                    "256" => "ymmm256b64",
                    "512" => "zmmm512b64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    for (int i = 0; i < Vector{bitness}<{type}>.Count; i++)
                    {{
                        if (!HasBitSetInK1(i))
                        {{
                            result = result.WithElement(
                                i,
                                {algorithm.Replace('?', '+')});
                        }}
                    }}

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfmaddSS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfmadd132ss");
        Algorithm("(a * c) ? b", "float", "Vfmadd132ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd132ss.g.cs");

        CodeStart(sb, "Vfmadd213ss");
        Algorithm("(b * a) ? c", "float", "Vfmadd213ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd213ss.g.cs");

        CodeStart(sb, "Vfmadd231ss");
        Algorithm("(c * b) ? a", "float", "Vfmadd231ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd231ss.g.cs");

        // Double
        CodeStart(sb, "Vfmadd132sd");
        Algorithm("(a * c) ? b", "double", "Vfmadd132sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd132sd.g.cs");

        CodeStart(sb, "Vfmadd213sd");
        Algorithm("(b * a) ? c", "double", "Vfmadd213sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd213sd.g.cs");

        CodeStart(sb, "Vfmadd231sd");
        Algorithm("(c * b) ? a", "double", "Vfmadd231sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd231sd.g.cs");

        // Half
        CodeStart(sb, "Vfmadd132sh");
        Algorithm("(a * c) ? b", "Half", "Vfmadd132sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd132sh.g.cs");

        CodeStart(sb, "Vfmadd213sh");
        Algorithm("(b * a) ? c", "Half", "Vfmadd213sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd213sh.g.cs");

        CodeStart(sb, "Vfmadd231sh");
        Algorithm("(c * b) ? a", "Half", "Vfmadd231sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmadd231sh.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm16_er",
                    "256" => "ymmm16_er",
                    "512" => "zmmm16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm32_er",
                    "256" => "ymmm32_er",
                    "512" => "zmmm32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm64_er",
                    "256" => "ymmm64_er",
                    "512" => "zmmm64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    result = result.WithElement(0, {algorithm.Replace('?', '+').Replace('i', '0')});

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfmsubPS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfmsub132ps");
        Algorithm("(a * c) ? b", "float", "Vfmsub132ps", "128", "X");
        Algorithm("(a * c) ? b", "float", "Vfmsub132ps", "256", "Y");
        Algorithm("(a * c) ? b", "float", "Vfmsub132ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub132ps.g.cs");

        CodeStart(sb, "Vfmsub213ps");
        Algorithm("(b * a) ? c", "float", "Vfmsub213ps", "128", "X");
        Algorithm("(b * a) ? c", "float", "Vfmsub213ps", "256", "Y");
        Algorithm("(b * a) ? c", "float", "Vfmsub213ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub213ps.g.cs");

        CodeStart(sb, "Vfmsub231ps");
        Algorithm("(c * b) ? a", "float", "Vfmsub231ps", "128", "X");
        Algorithm("(c * b) ? a", "float", "Vfmsub231ps", "256", "Y");
        Algorithm("(c * b) ? a", "float", "Vfmsub231ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub231ps.g.cs");

        // Double
        CodeStart(sb, "Vfmsub132pd");
        Algorithm("(a * c) ? b", "double", "Vfmsub132pd", "128", "X");
        Algorithm("(a * c) ? b", "double", "Vfmsub132pd", "256", "Y");
        Algorithm("(a * c) ? b", "double", "Vfmsub132pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub132pd.g.cs");

        CodeStart(sb, "Vfmsub213pd");
        Algorithm("(b * a) ? c", "double", "Vfmsub213pd", "128", "X");
        Algorithm("(b * a) ? c", "double", "Vfmsub213pd", "256", "Y");
        Algorithm("(b * a) ? c", "double", "Vfmsub213pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub213pd.g.cs");

        CodeStart(sb, "Vfmsub231pd");
        Algorithm("(c * b) ? a", "double", "Vfmsub231pd", "128", "X");
        Algorithm("(c * b) ? a", "double", "Vfmsub231pd", "256", "Y");
        Algorithm("(c * b) ? a", "double", "Vfmsub231pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub231pd.g.cs");

        // Half
        CodeStart(sb, "Vfmsub132ph");
        Algorithm("(a * c) ? b", "Half", "Vfmsub132ph", "128", "X");
        Algorithm("(a * c) ? b", "Half", "Vfmsub132ph", "256", "Y");
        Algorithm("(a * c) ? b", "Half", "Vfmsub132ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub132ph.g.cs");

        CodeStart(sb, "Vfmsub213ph");
        Algorithm("(b * a) ? c", "Half", "Vfmsub213ph", "128", "X");
        Algorithm("(b * a) ? c", "Half", "Vfmsub213ph", "256", "Y");
        Algorithm("(b * a) ? c", "Half", "Vfmsub213ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub213ph.g.cs");

        CodeStart(sb, "Vfmsub231ph");
        Algorithm("(c * b) ? a", "Half", "Vfmsub231ph", "128", "X");
        Algorithm("(c * b) ? a", "Half", "Vfmsub231ph", "256", "Y");
        Algorithm("(c * b) ? a", "Half", "Vfmsub231ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub231ph.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm128b16",
                    "256" => "ymmm256b16",
                    "512" => "zmmm512b16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm128b32",
                    "256" => "ymmm256b32",
                    "512" => "zmmm512b32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm128b64",
                    "256" => "ymmm256b64",
                    "512" => "zmmm512b64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    for (int i = 0; i < Vector{bitness}<{type}>.Count; i++)
                    {{
                        if (!HasBitSetInK1(i))
                        {{
                            result = result.WithElement(
                                i,
                                {algorithm.Replace('?', '-')});
                        }}
                    }}

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfmsubSS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfmsub132ss");
        Algorithm("(a * c) ? b", "float", "Vfmsub132ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub132ss.g.cs");

        CodeStart(sb, "Vfmsub213ss");
        Algorithm("(b * a) ? c", "float", "Vfmsub213ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub213ss.g.cs");

        CodeStart(sb, "Vfmsub231ss");
        Algorithm("(c * b) ? a", "float", "Vfmsub231ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub231ss.g.cs");

        // Double
        CodeStart(sb, "Vfmsub132sd");
        Algorithm("(a * c) ? b", "double", "Vfmsub132sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub132sd.g.cs");

        CodeStart(sb, "Vfmsub213sd");
        Algorithm("(b * a) ? c", "double", "Vfmsub213sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub213sd.g.cs");

        CodeStart(sb, "Vfmsub231sd");
        Algorithm("(c * b) ? a", "double", "Vfmsub231sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub231sd.g.cs");

        // Half
        CodeStart(sb, "Vfmsub132sh");
        Algorithm("(a * c) ? b", "Half", "Vfmsub132sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub132sh.g.cs");

        CodeStart(sb, "Vfmsub213sh");
        Algorithm("(b * a) ? c", "Half", "Vfmsub213sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub213sh.g.cs");

        CodeStart(sb, "Vfmsub231sh");
        Algorithm("(c * b) ? a", "Half", "Vfmsub231sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfmsub231sh.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm16_er",
                    "256" => "ymmm16_er",
                    "512" => "zmmm16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm32_er",
                    "256" => "ymmm32_er",
                    "512" => "zmmm32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm64_er",
                    "256" => "ymmm64_er",
                    "512" => "zmmm64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    result = result.WithElement(0, {algorithm.Replace('?', '-').Replace('i', '0')});

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfnmaddPS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfnmadd132ps");
        Algorithm("(a * c) ? b", "float", "Vfnmadd132ps", "128", "X");
        Algorithm("(a * c) ? b", "float", "Vfnmadd132ps", "256", "Y");
        Algorithm("(a * c) ? b", "float", "Vfnmadd132ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd132ps.g.cs");

        CodeStart(sb, "Vfnmadd213ps");
        Algorithm("(b * a) ? c", "float", "Vfnmadd213ps", "128", "X");
        Algorithm("(b * a) ? c", "float", "Vfnmadd213ps", "256", "Y");
        Algorithm("(b * a) ? c", "float", "Vfnmadd213ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd213ps.g.cs");

        CodeStart(sb, "Vfnmadd231ps");
        Algorithm("(c * b) ? a", "float", "Vfnmadd231ps", "128", "X");
        Algorithm("(c * b) ? a", "float", "Vfnmadd231ps", "256", "Y");
        Algorithm("(c * b) ? a", "float", "Vfnmadd231ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd231ps.g.cs");

        // Double
        CodeStart(sb, "Vfnmadd132pd");
        Algorithm("(a * c) ? b", "double", "Vfnmadd132pd", "128", "X");
        Algorithm("(a * c) ? b", "double", "Vfnmadd132pd", "256", "Y");
        Algorithm("(a * c) ? b", "double", "Vfnmadd132pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd132pd.g.cs");

        CodeStart(sb, "Vfnmadd213pd");
        Algorithm("(b * a) ? c", "double", "Vfnmadd213pd", "128", "X");
        Algorithm("(b * a) ? c", "double", "Vfnmadd213pd", "256", "Y");
        Algorithm("(b * a) ? c", "double", "Vfnmadd213pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd213pd.g.cs");

        CodeStart(sb, "Vfnmadd231pd");
        Algorithm("(c * b) ? a", "double", "Vfnmadd231pd", "128", "X");
        Algorithm("(c * b) ? a", "double", "Vfnmadd231pd", "256", "Y");
        Algorithm("(c * b) ? a", "double", "Vfnmadd231pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd231pd.g.cs");

        // Half
        CodeStart(sb, "Vfnmadd132ph");
        Algorithm("(a * c) ? b", "Half", "Vfnmadd132ph", "128", "X");
        Algorithm("(a * c) ? b", "Half", "Vfnmadd132ph", "256", "Y");
        Algorithm("(a * c) ? b", "Half", "Vfnmadd132ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd132ph.g.cs");

        CodeStart(sb, "Vfnmadd213ph");
        Algorithm("(b * a) ? c", "Half", "Vfnmadd213ph", "128", "X");
        Algorithm("(b * a) ? c", "Half", "Vfnmadd213ph", "256", "Y");
        Algorithm("(b * a) ? c", "Half", "Vfnmadd213ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd213ph.g.cs");

        CodeStart(sb, "Vfnmadd231ph");
        Algorithm("(c * b) ? a", "Half", "Vfnmadd231ph", "128", "X");
        Algorithm("(c * b) ? a", "Half", "Vfnmadd231ph", "256", "Y");
        Algorithm("(c * b) ? a", "Half", "Vfnmadd231ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd231ph.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm128b16",
                    "256" => "ymmm256b16",
                    "512" => "zmmm512b16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm128b32",
                    "256" => "ymmm256b32",
                    "512" => "zmmm512b32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm128b64",
                    "256" => "ymmm256b64",
                    "512" => "zmmm512b64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    for (int i = 0; i < Vector{bitness}<{type}>.Count; i++)
                    {{
                        if (!HasBitSetInK1(i))
                        {{
                            result = result.WithElement(
                                i,
                                {algorithm.Replace('?', '+')
                                .Replace("src1[i]", $"({type})(-src1[i])")});
                        }}
                    }}

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfnmaddSS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfnmadd132ss");
        Algorithm("(a * c) ? b", "float", "Vfnmadd132ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd132ss.g.cs");

        CodeStart(sb, "Vfnmadd213ss");
        Algorithm("(b * a) ? c", "float", "Vfnmadd213ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd213ss.g.cs");

        CodeStart(sb, "Vfnmadd231ss");
        Algorithm("(c * b) ? a", "float", "Vfnmadd231ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd231ss.g.cs");

        // Double
        CodeStart(sb, "Vfnmadd132sd");
        Algorithm("(a * c) ? b", "double", "Vfnmadd132sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd132sd.g.cs");

        CodeStart(sb, "Vfnmadd213sd");
        Algorithm("(b * a) ? c", "double", "Vfnmadd213sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd213sd.g.cs");

        CodeStart(sb, "Vfnmadd231sd");
        Algorithm("(c * b) ? a", "double", "Vfnmadd231sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd231sd.g.cs");

        // Half
        CodeStart(sb, "Vfnmadd132sh");
        Algorithm("(a * c) ? b", "Half", "Vfnmadd132sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd132sh.g.cs");

        CodeStart(sb, "Vfnmadd213sh");
        Algorithm("(b * a) ? c", "Half", "Vfnmadd213sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd213sh.g.cs");

        CodeStart(sb, "Vfnmadd231sh");
        Algorithm("(c * b) ? a", "Half", "Vfnmadd231sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmadd231sh.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm16_er",
                    "256" => "ymmm16_er",
                    "512" => "zmmm16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm32_er",
                    "256" => "ymmm32_er",
                    "512" => "zmmm32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm64_er",
                    "256" => "ymmm64_er",
                    "512" => "zmmm64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    result = result.WithElement(0, {algorithm.Replace('?', '+').Replace('i', '0').Replace("src1[i]", $"({type})(-src1[i])")});

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfnmsubPS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfnmsub132ps");
        Algorithm("(a * c) ? b", "float", "Vfnmsub132ps", "128", "X");
        Algorithm("(a * c) ? b", "float", "Vfnmsub132ps", "256", "Y");
        Algorithm("(a * c) ? b", "float", "Vfnmsub132ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub132ps.g.cs");

        CodeStart(sb, "Vfnmsub213ps");
        Algorithm("(b * a) ? c", "float", "Vfnmsub213ps", "128", "X");
        Algorithm("(b * a) ? c", "float", "Vfnmsub213ps", "256", "Y");
        Algorithm("(b * a) ? c", "float", "Vfnmsub213ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub213ps.g.cs");

        CodeStart(sb, "Vfnmsub231ps");
        Algorithm("(c * b) ? a", "float", "Vfnmsub231ps", "128", "X");
        Algorithm("(c * b) ? a", "float", "Vfnmsub231ps", "256", "Y");
        Algorithm("(c * b) ? a", "float", "Vfnmsub231ps", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub231ps.g.cs");

        // Double
        CodeStart(sb, "Vfnmsub132pd");
        Algorithm("(a * c) ? b", "double", "Vfnmsub132pd", "128", "X");
        Algorithm("(a * c) ? b", "double", "Vfnmsub132pd", "256", "Y");
        Algorithm("(a * c) ? b", "double", "Vfnmsub132pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub132pd.g.cs");

        CodeStart(sb, "Vfnmsub213pd");
        Algorithm("(b * a) ? c", "double", "Vfnmsub213pd", "128", "X");
        Algorithm("(b * a) ? c", "double", "Vfnmsub213pd", "256", "Y");
        Algorithm("(b * a) ? c", "double", "Vfnmsub213pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub213pd.g.cs");

        CodeStart(sb, "Vfnmsub231pd");
        Algorithm("(c * b) ? a", "double", "Vfnmsub231pd", "128", "X");
        Algorithm("(c * b) ? a", "double", "Vfnmsub231pd", "256", "Y");
        Algorithm("(c * b) ? a", "double", "Vfnmsub231pd", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub231pd.g.cs");

        // Half
        CodeStart(sb, "Vfnmsub132ph");
        Algorithm("(a * c) ? b", "Half", "Vfnmsub132ph", "128", "X");
        Algorithm("(a * c) ? b", "Half", "Vfnmsub132ph", "256", "Y");
        Algorithm("(a * c) ? b", "Half", "Vfnmsub132ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub132ph.g.cs");

        CodeStart(sb, "Vfnmsub213ph");
        Algorithm("(b * a) ? c", "Half", "Vfnmsub213ph", "128", "X");
        Algorithm("(b * a) ? c", "Half", "Vfnmsub213ph", "256", "Y");
        Algorithm("(b * a) ? c", "Half", "Vfnmsub213ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub213ph.g.cs");

        CodeStart(sb, "Vfnmsub231ph");
        Algorithm("(c * b) ? a", "Half", "Vfnmsub231ph", "128", "X");
        Algorithm("(c * b) ? a", "Half", "Vfnmsub231ph", "256", "Y");
        Algorithm("(c * b) ? a", "Half", "Vfnmsub231ph", "512", "Z");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub231ph.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm128b16",
                    "256" => "ymmm256b16",
                    "512" => "zmmm512b16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm128b32",
                    "256" => "ymmm256b32",
                    "512" => "zmmm512b32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm128b64",
                    "256" => "ymmm256b64",
                    "512" => "zmmm512b64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    for (int i = 0; i < Vector{bitness}<{type}>.Count; i++)
                    {{
                        if (!HasBitSetInK1(i))
                        {{
                            result = result.WithElement(
                                i,
                                {algorithm.Replace('?', '-')
                                .Replace("src1[i]", $"({type})(-src1[i])")});
                        }}
                    }}

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    public void VfnmsubSS()
    {
        var sb = new StringBuilder();

        // Single
        CodeStart(sb, "Vfnmsub132ss");
        Algorithm("(a * c) ? b", "float", "Vfnmsub132ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub132ss.g.cs");

        CodeStart(sb, "Vfnmsub213ss");
        Algorithm("(b * a) ? c", "float", "Vfnmsub213ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub213ss.g.cs");

        CodeStart(sb, "Vfnmsub231ss");
        Algorithm("(c * b) ? a", "float", "Vfnmsub231ss", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub231ss.g.cs");

        // Double
        CodeStart(sb, "Vfnmsub132sd");
        Algorithm("(a * c) ? b", "double", "Vfnmsub132sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub132sd.g.cs");

        CodeStart(sb, "Vfnmsub213sd");
        Algorithm("(b * a) ? c", "double", "Vfnmsub213sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub213sd.g.cs");

        CodeStart(sb, "Vfnmsub231sd");
        Algorithm("(c * b) ? a", "double", "Vfnmsub231sd", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub231sd.g.cs");

        // Half
        CodeStart(sb, "Vfnmsub132sh");
        Algorithm("(a * c) ? b", "Half", "Vfnmsub132sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub132sh.g.cs");

        CodeStart(sb, "Vfnmsub213sh");
        Algorithm("(b * a) ? c", "Half", "Vfnmsub213sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub213sh.g.cs");

        CodeStart(sb, "Vfnmsub231sh");
        Algorithm("(c * b) ? a", "Half", "Vfnmsub231sh", "128", "X");
        CodeEnd(sb);
        ExportFile(sb, "Vfnmsub231sh.g.cs");

        void Algorithm(string formula, string type, string instructionName, string bitness, string xyz)
        {
            string lastParameter = type switch
            {
                "Half" => bitness switch
                {
                    "128" => "xmmm16_er",
                    "256" => "ymmm16_er",
                    "512" => "zmmm16_er",
                    _ => "???"
                },
                "float" => bitness switch
                {
                    "128" => "xmmm32_er",
                    "256" => "ymmm32_er",
                    "512" => "zmmm32_er",
                    _ => "???"
                },
                "double" => bitness switch
                {
                    "128" => "xmmm64_er",
                    "256" => "ymmm64_er",
                    "512" => "zmmm64_er",
                    _ => "???"
                },
                _ => "???"
            };
            string middleParameters = bitness switch
            {
                "128" => "_xmm_k1z_xmm_",
                "256" => "_ymm_k1z_ymm_",
                "512" => "_zmm_k1z_zmm_",
                _ => "???"
            };
            string algorithm = formula
                .Replace("c", "result[i]")
                .Replace("a", "src1[i]")
                .Replace("b", "src2[i]");
            sb.AppendLine($@"
                case Code.EVEX_{instructionName}{middleParameters}{lastParameter}:
                {{
                    Vector{bitness}<{type}> src1 = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(1)).As<float, {type}>();
                    Vector{bitness}<{type}> src2 = Evaluate{xyz}mmFromInstruction(in instruction, 2).As<float, {type}>();

                    Vector{bitness}<{type}> result = this.ProcessorRegisters.Evaluate{xyz}mm(instruction.GetOpRegister(0)).As<float, {type}>();
                    result = result.WithElement(0, {algorithm.Replace('?', '-').Replace('i', '0').Replace("src1[i]", $"({type})(-src1[i])")});

                    if (instruction.ZeroingMasking)
                        result = result.K1z(0, this.ProcessorRegisters.EvaluateK(instruction.OpMask));

                    this.ProcessorRegisters.Set{xyz}mm(instruction.GetOpRegister(0), result.As<{type}, float>());
                    break;
                }}");
        }
    }

    static void CodeStart(StringBuilder sb, string instructionName)
    {
        sb.Append($@"// This file was auto-generated.
// See /eng/BuildTools/X64/FMA3Generator.

using Iced.Intel;
using Machine.Utility;
using Machine.X64.Component;
using System.Runtime.Intrinsics;

namespace Machine.X64.Runtime;

public partial class CpuRuntime
{{
    private void {instructionName}(in Instruction instruction)
    {{
        switch (instruction.Code)
        {{");
    }

    static void CodeEnd(StringBuilder sb)
    {
        sb.Append($@"
            default:
                ReportInvalidCodeUnderMnemonic(instruction.Code, instruction.Mnemonic);
                break;
        }}
    }}
}}
");
    }
}
