﻿using System.Text;

namespace RegistersXmlToCSharp;

internal static class CodeGenerator
{
    public static string Generate(DocumentSkeleton skeleton)
    {
        var stringBuilder = new StringBuilder();
        AppendCompiled(stringBuilder, skeleton);
        return stringBuilder.ToString();
    }

    private static void AppendCompiled(StringBuilder stringBuilder, DocumentSkeleton skeleton)
    {
        AppendNamespace(stringBuilder, skeleton);
    }

    private static void AppendNamespace(StringBuilder stringBuilder, DocumentSkeleton skeleton)
    {
        string code = GetClassAndInterface(skeleton);
        stringBuilder.Append($@"// <auto-generated>
// This file was generated automatically. 🖥️
// See /eng/BuildTools/X64/RegistersXmlToCSharp
// </auto-generated>

using System.Runtime.Intrinsics;

namespace {skeleton.Namespace}
{{
{code}
}}");
    }

    private static string GetClassAndInterface(DocumentSkeleton skeleton)
    {
        var sb = new StringBuilder();

        if (skeleton.AllowRegisterInterface)
        {
            AppendInterface(sb, skeleton);
        }
        AppendClass(sb, skeleton);

        return sb.ToString();
    }

    private static void AppendInterface(StringBuilder sb, DocumentSkeleton skeleton)
    {
        sb.AppendLine($"public interface I{skeleton.TypeName}");
        sb.AppendLine($"{{");
        foreach (RegisterGroupNode group in skeleton.Children)
        {
            switch (group.Name)
            {
                case "GeneralPurpose":
                    {
                        GeneralPurposeGroupNode groupNode = (GeneralPurposeGroupNode)group;
                        foreach (IBoundRegisterOrDefinition definition in groupNode.Children)
                        {
                            if (definition is BoundRegister boundRegister)
                            {
                                RegisterBinding binding = boundRegister.Bindings.First();
                                sb.AppendLine($"    /// <summary>");
                                sb.AppendLine($"    /// Represents the {boundRegister.Name} register.");
                                sb.AppendLine($"    /// </summary>");
                                sb.AppendLine($"    /// <remarks>");
                                sb.AppendLine($"    /// This register is bound to the {binding.TargetName} register.");
                                sb.AppendLine($"    /// </remarks>");
                                sb.AppendLine($"    public {ClassifyBitnessToType(binding.Bitness / 2)} {boundRegister.MetadataName} {{ get; set; }}");
                            }
                            else if (definition is RegisterDefinition registerDef)
                            {
                                AppendRegisterDefinitionWithFourSpaceIndent(sb, registerDef);
                            }
                        }
                        break;
                    }

                case "Segment":
                    {
                        SegmentGroupNode segment = (SegmentGroupNode)group;
                        foreach (RegisterDefinition definition in segment.Children)
                            AppendRegisterDefinitionWithFourSpaceIndent(sb, definition);
                        break;
                    }

                case "VectorRegisters":
                    {
                        VectorRegistersGroupNode vector = (VectorRegistersGroupNode)group;
                        foreach (BaseVectorRegister register in vector.Children)
                        {
                            sb.AppendLine($"    /// <summary>");
                            sb.AppendLine($"    /// Represents the {register.Name} vector register.");
                            sb.AppendLine($"    /// </summary>");
                            sb.AppendLine($"    public Vector512<float> {register.MetadataName} {{ get; set; }}");

                            if (register.AlsoCreateYmm)
                            {
                                sb.AppendLine($"    /// <summary>");
                                sb.AppendLine($"    /// Represents the {ZmmNameToYmm(register.Name)} vector register.");
                                sb.AppendLine($"    /// </summary>");
                                sb.AppendLine($"    /// <remarks>");
                                sb.AppendLine($"    /// This register is part of the {register.Name} register.");
                                sb.AppendLine($"    /// </remarks>");
                                sb.AppendLine($"    public Vector256<float> {ZmmNameToYmm(register.MetadataName)} {{ get; set; }}");

                                if (register.AlsoCreateXmm)
                                {
                                    sb.AppendLine($"    /// <summary>");
                                    sb.AppendLine($"    /// Represents the {ZmmNameToXmm(register.Name)} vector register.");
                                    sb.AppendLine($"    /// </summary>");
                                    sb.AppendLine($"    /// <remarks>");
                                    sb.AppendLine($"    /// This register is part of the {ZmmNameToYmm(register.Name)} register.");
                                    sb.AppendLine($"    /// </remarks>");
                                    sb.AppendLine($"    public Vector128<float> {ZmmNameToXmm(register.MetadataName)} {{ get; set; }}");
                                }
                            }
                        }
                    }
                    break;

                case "K":
                    {
                        KGroupNode kGroup = (KGroupNode)group;
                        foreach (RegisterDefinition definition in kGroup.Children)
                            AppendRegisterDefinitionWithFourSpaceIndent(sb, definition);
                        break;
                    }

                case "Other":
                    {
                        OtherGroupNode other = (OtherGroupNode)group;
                        foreach (RegisterDefinition definition in other.Children)
                            AppendRegisterDefinitionWithFourSpaceIndent(sb, definition);
                        break;
                    }

                case "CRFlags":
                    {
                        CRFlagsGroupNode crFlags = (CRFlagsGroupNode)group;
                        foreach (CRFlagDefinitions defs in crFlags.Children)
                        {
                            string cr = $"CR{defs.CRIndex}";
                            foreach (CRFlag flag in defs.Flags)
                            {
                                sb.AppendLine("    /// <summary>");
                                sb.AppendLine($"    /// From the <see cref=\"{cr}\" /> register, gets/sets the <c>{flag.Name}</c> ({flag.AlsoKnownAs}) flag.");
                                sb.AppendLine("    /// </summary>");
                                sb.AppendLine($"    public bool {cr}{flag.Name} {{ get; set; }}");
                            }
                        }
                        break;
                    }

                case "CpuFlags":
                    {
                        CpuFlagsGroupNode cpuFlags = (CpuFlagsGroupNode)group;
                        foreach (BoundFlag boundFlag in cpuFlags.Children)
                        {
                            sb.AppendLine("    /// <summary>");
                            sb.AppendLine($"    /// Gets/sets the <c>{boundFlag.Name}</c> ({boundFlag.AlsoKnownAs}) flag.");
                            sb.AppendLine("    /// </summary>");
                            sb.AppendLine($"    public bool RFlags{boundFlag.Name} {{ get; set; }}");
                        }
                        break;
                    }
            }
        }
        sb.AppendLine($"}}");
    }

    private static void AppendClass(StringBuilder sb, DocumentSkeleton skeleton)
    {
        sb.AppendLine($"public sealed class {skeleton.TypeName} {(skeleton.AllowRegisterInterface ? $" : I{skeleton.TypeName}" : "")}");
        sb.AppendLine("{");

        foreach (RegisterGroupNode group in skeleton.Children)
        {
            switch (group.Name)
            {
                case "GeneralPurpose":
                    {
                        GeneralPurposeGroupNode groupNode = (GeneralPurposeGroupNode)group;
                        foreach (IBoundRegisterOrDefinition definition in groupNode.Children)
                        {
                            if (definition is BoundRegister boundRegister)
                            {
                                RegisterBinding binding = boundRegister.Bindings.First();
                                sb.AppendLine($"    /// <summary>");
                                sb.AppendLine($"    /// Represents the {boundRegister.Name} register.");
                                sb.AppendLine($"    /// </summary>");
                                sb.AppendLine($"    /// <remarks>");
                                sb.AppendLine($"    /// This register is bound to the {binding.TargetName} register.");
                                sb.AppendLine($"    /// </remarks>");
                                sb.AppendLine($"    public {ClassifyBitnessToType(binding.Bitness / 2)} {boundRegister.MetadataName}");
                                sb.AppendLine("    {");
                                sb.AppendLine($"        get => ({ClassifyBitnessToType(binding.Bitness / 2)})({binding.TargetMetadataName} & {ClassifyBitnessToBitMask(binding.Bitness)});");
                                sb.AppendLine($"        set => {binding.TargetMetadataName} = ({ClassifyBitnessToType(binding.Bitness)})(({binding.TargetMetadataName} & {ClassifyBitnessToBitMask2(binding.Bitness)}) | value);");
                                sb.AppendLine("    }");
                            }
                            else if (definition is RegisterDefinition registerDef)
                            {
                                AppendRegisterDefinitionWithFourSpaceIndent(sb, registerDef);
                            }
                        }
                        break;
                    }

                case "Segment":
                    {
                        SegmentGroupNode segment = (SegmentGroupNode)group;
                        foreach (RegisterDefinition definition in segment.Children)
                            AppendRegisterDefinitionWithFourSpaceIndent(sb, definition);
                        break;
                    }

                case "VectorRegisters":
                    {
                        VectorRegistersGroupNode vector = (VectorRegistersGroupNode)group;
                        foreach (BaseVectorRegister register in vector.Children)
                        {
                            sb.AppendLine($"    /// <summary>");
                            sb.AppendLine($"    /// Represents the {register.Name} vector register.");
                            sb.AppendLine($"    /// </summary>");
                            sb.AppendLine($"    public Vector512<float> {register.MetadataName} {{ get; set; }}");

                            if (register.AlsoCreateYmm)
                            {
                                sb.AppendLine($"    /// <summary>");
                                sb.AppendLine($"    /// Represents the {ZmmNameToYmm(register.Name)} vector register.");
                                sb.AppendLine($"    /// </summary>");
                                sb.AppendLine($"    /// <remarks>");
                                sb.AppendLine($"    /// This register is part of the {register.Name} register.");
                                sb.AppendLine($"    /// </remarks>");
                                sb.AppendLine($"    public Vector256<float> {ZmmNameToYmm(register.MetadataName)}");
                                sb.AppendLine($"    {{");
                                sb.AppendLine($"        get => {register.MetadataName}.GetLower();");
                                sb.AppendLine($"        set => {register.MetadataName} = {register.MetadataName}.WithLower(value);");
                                sb.AppendLine($"    }}");

                                if (register.AlsoCreateXmm)
                                {
                                    sb.AppendLine($"    /// <summary>");
                                    sb.AppendLine($"    /// Represents the {ZmmNameToXmm(register.Name)} vector register.");
                                    sb.AppendLine($"    /// </summary>");
                                    sb.AppendLine($"    /// <remarks>");
                                    sb.AppendLine($"    /// This register is part of the {ZmmNameToYmm(register.Name)} register.");
                                    sb.AppendLine($"    /// </remarks>");
                                    sb.AppendLine($"    public Vector128<float> {ZmmNameToXmm(register.MetadataName)}");
                                    sb.AppendLine($"    {{");
                                    sb.AppendLine($"        get => {ZmmNameToYmm(register.MetadataName)}.GetLower();");
                                    sb.AppendLine($"        set => {ZmmNameToYmm(register.MetadataName)} = {ZmmNameToYmm(register.MetadataName)}.WithLower(value);");
                                    sb.AppendLine($"    }}");
                                }
                            }
                        }
                    }
                    break;

                case "K":
                    {
                        KGroupNode kGroup = (KGroupNode)group;
                        foreach (RegisterDefinition definition in kGroup.Children)
                            AppendRegisterDefinitionWithFourSpaceIndent(sb, definition);
                        break;
                    }

                case "Other":
                    {
                        OtherGroupNode other = (OtherGroupNode)group;
                        foreach (RegisterDefinition definition in other.Children)
                            AppendRegisterDefinitionWithFourSpaceIndent(sb, definition);
                        break;
                    }

                case "CRFlags":
                    {
                        CRFlagsGroupNode crFlags = (CRFlagsGroupNode)group;
                        foreach (CRFlagDefinitions defs in crFlags.Children)
                        {
                            string cr = $"CR{defs.CRIndex}";
                            foreach (CRFlag flag in defs.Flags)
                            {
                                sb.AppendLine("    /// <summary>");
                                sb.AppendLine($"    /// From the <see cref=\"{cr}\" /> register, gets/sets the <c>{flag.Name}</c> ({flag.AlsoKnownAs}) flag.");
                                sb.AppendLine("    /// </summary>");
                                sb.AppendLine($"    public bool {cr}{flag.Name}");
                                sb.AppendLine("    {");
                                sb.AppendLine($"        get => (byte)({cr} & 0x{1 << flag.Bit:X}) != 0;");
                                sb.AppendLine($"        set");
                                sb.AppendLine($"        {{");
                                sb.AppendLine("            if (value)");
                                sb.AppendLine("            {");
                                sb.AppendLine($"                {cr} |= 1UL << {flag.Bit};");
                                sb.AppendLine("            }");
                                sb.AppendLine("            else");
                                sb.AppendLine("            {");
                                sb.AppendLine($"                {cr} &= ~(1UL << {flag.Bit});");
                                sb.AppendLine("            }");
                                sb.AppendLine("        }");
                                sb.AppendLine("    }");
                            }
                        }
                        break;
                    }

                case "CpuFlags":
                    {
                        CpuFlagsGroupNode cpuFlags = (CpuFlagsGroupNode)group;
                        foreach (BoundFlag boundFlag in cpuFlags.Children)
                        {
                            sb.AppendLine("    /// <summary>");
                            sb.AppendLine($"    /// Gets/sets the <c>{boundFlag.Name}</c> ({boundFlag.AlsoKnownAs}) flag.");
                            sb.AppendLine("    /// </summary>");
                            sb.AppendLine($"    public bool RFlags{boundFlag.Name}");
                            sb.AppendLine("    {");
                            sb.AppendLine($"        get => (byte)(RFlags & 0x{1 << boundFlag.Bit:X}) != 0;");
                            sb.AppendLine($"        set");
                            sb.AppendLine($"        {{");
                            sb.AppendLine("            if (value)");
                            sb.AppendLine("            {");
                            sb.AppendLine($"                RFlags |= 1UL << {boundFlag.Bit};");
                            sb.AppendLine("            }");
                            sb.AppendLine("            else");
                            sb.AppendLine("            {");
                            sb.AppendLine($"                RFlags &= ~(1UL << {boundFlag.Bit});");
                            sb.AppendLine("            }");
                            sb.AppendLine("        }");
                            sb.AppendLine("    }");
                        }
                        break;
                    }

                case "GeneralPurposePart":
                    {
                        GeneralPurposePartGroupNode node = (GeneralPurposePartGroupNode)group;
                        foreach (GeneralPurposeRegisterPart part in node.Children)
                        {
                            if (part.Direction == "Top")
                            {
                                sb.AppendLine($"    /// <summary>");
                                sb.AppendLine($"    /// Represents the {part.Name} register.");
                                sb.AppendLine($"    /// </summary>");
                                sb.AppendLine($"    /// <remarks>");
                                sb.AppendLine($"    /// This register is bound to the {part.TargetName} register.");
                                sb.AppendLine($"    /// </remarks>");
                                sb.AppendLine($"    public {ClassifyBitnessToType(part.BitSplit)} {part.MetadataName}");
                                sb.AppendLine($"    {{");
                                sb.AppendLine($"        get => ({ClassifyBitnessToType(part.BitSplit)})({part.TargetMetadataName} >> {part.BitSplit});");
                                sb.AppendLine($"        set => {part.TargetMetadataName} = ({ClassifyBitnessToType(part.BitSplit * 2)})(({part.TargetMetadataName} & {ClassifyBitnessToBitMask(part.BitSplit * 2)}) | ({ClassifyBitnessToType(part.BitSplit)})value << {part.BitSplit});");
                                sb.AppendLine($"    }}");
                            }
                            else if (part.Direction == "Bottom")
                            {
                                sb.AppendLine($"    /// <summary>");
                                sb.AppendLine($"    /// Represents the {part.Name} register.");
                                sb.AppendLine($"    /// </summary>");
                                sb.AppendLine($"    /// <remarks>");
                                sb.AppendLine($"    /// This register is bound to the {part.TargetName} register.");
                                sb.AppendLine($"    /// </remarks>");
                                sb.AppendLine($"    public {ClassifyBitnessToType(part.BitSplit)} {part.MetadataName}");
                                sb.AppendLine("    {");
                                sb.AppendLine($"        get => ({ClassifyBitnessToType(part.BitSplit)})({part.TargetMetadataName} & {ClassifyBitnessToBitMask(part.BitSplit * 2)});");
                                sb.AppendLine($"        set => {part.TargetMetadataName} = ({ClassifyBitnessToType(part.BitSplit)})(({part.TargetMetadataName} & {ClassifyBitnessToBitMask2(part.BitSplit * 2)}) | value);");
                                sb.AppendLine("    }");
                            }
                        }
                        break;
                    }
            }
        }

        sb.AppendLine("}");
    }

    private static void AppendRegisterDefinitionWithFourSpaceIndent(StringBuilder sb, RegisterDefinition registerDef)
    {
        string name = registerDef.Name.StartsWith("Double:") ? registerDef.Name["Double:".Length..] : registerDef.Name;
        string type = registerDef.Name.StartsWith("Double:") ? "double" : ClassifyBitnessToType(registerDef.Bitness);

        sb.AppendLine($"    /// <summary>");
        sb.AppendLine($"    /// Represents the {name} register.");
        sb.AppendLine($"    /// </summary>");
        sb.AppendLine($"    public {type} {registerDef.MetadataName} {{ get; set; }}");
    }

    private static string ZmmNameToYmm(string zmm)
    {
        var sb = new StringBuilder(zmm);
        if (string.IsNullOrEmpty(zmm))
            return string.Empty;

        if (zmm.First() == 'Z')
            sb[0] = 'Y';

        return sb.ToString();
    }

    private static string ZmmNameToXmm(string zmm)
    {
        var sb = new StringBuilder(zmm);
        if (string.IsNullOrEmpty(zmm))
            return string.Empty;

        if (zmm.First() == 'Z')
            sb[0] = 'X';

        return sb.ToString();
    }

    private static string ClassifyBitnessToType(int bitness)
    {
        return bitness switch
        {
            8 => "byte",
            16 => "ushort",
            32 => "uint",
            64 => "ulong",
            _ => "???"
        };
    }

    private static string ClassifyBitnessToBitMask(int bitness)
    {
        return bitness switch
        {
            64 => "0x00000000FFFFFFFF",
            32 => "0x0000FFFF",
            16 => "0x00FF",
            8 => "0x0F",
            _ => "???"
        };
    }

    private static string ClassifyBitnessToBitMask2(int bitness)
    {
        return bitness switch
        {
            64 => "0xFFFFFFFF00000000",
            32 => "0xFFFF0000",
            16 => "0xFF00",
            8 => "0xF0",
            _ => "???"
        };
    }
}
