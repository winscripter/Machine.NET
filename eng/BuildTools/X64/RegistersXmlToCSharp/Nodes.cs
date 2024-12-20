namespace RegistersXmlToCSharp;

internal interface IRegisterGroupNode
{
}

internal abstract class RegisterGroupNode : IRegisterGroupNode
{
    public abstract string Name { get; }
}

internal sealed class GeneralPurposeGroupNode(IEnumerable<IBoundRegisterOrDefinition> children) : RegisterGroupNode
{
    public override string Name => "GeneralPurpose";

    public IEnumerable<IBoundRegisterOrDefinition> Children { get; } = children;
}

internal sealed class SegmentGroupNode(IEnumerable<RegisterDefinition> children) : RegisterGroupNode
{
    public IEnumerable<RegisterDefinition> Children { get; } = children;

    public override string Name => "Segment";
}

internal sealed class VectorRegistersGroupNode(IEnumerable<BaseVectorRegister> children) : RegisterGroupNode
{
    public IEnumerable<BaseVectorRegister> Children { get; } = children;

    public override string Name => "VectorRegisters";
}

internal sealed class KGroupNode(IEnumerable<RegisterDefinition> children) : RegisterGroupNode
{
    public IEnumerable<RegisterDefinition> Children { get; } = children;

    public override string Name => "K";
}

internal sealed class OtherGroupNode(IEnumerable<RegisterDefinition> children) : RegisterGroupNode
{
    public IEnumerable<RegisterDefinition> Children { get; } = children;

    public override string Name => "Other";
}

internal sealed class CRFlagsGroupNode(IEnumerable<CRFlagDefinitions> children) : RegisterGroupNode
{
    public IEnumerable<CRFlagDefinitions> Children { get; } = children;

    public override string Name => "CRFlags";
}

internal sealed class CpuFlagsGroupNode(IEnumerable<BoundFlag> children) : RegisterGroupNode
{
    public IEnumerable<BoundFlag> Children { get; } = children;

    public override string Name => "CpuFlags";
}

internal sealed class GeneralPurposePartGroupNode(IEnumerable<GeneralPurposeRegisterPart> children) : RegisterGroupNode
{
    public IEnumerable<GeneralPurposeRegisterPart> Children { get; } = children;

    public override string Name => "GeneralPurposePart";
}

internal interface IBoundRegisterOrDefinition
{
}

internal sealed record BoundFlag(int Bit, string Name, string AlsoKnownAs);
internal sealed record CRFlag(int Bit, string Name, string AlsoKnownAs);
internal sealed record GeneralPurposeRegisterPart(
    string TargetName,
    string TargetMetadataName,
    int BitSplit,
    string Direction,
    string Name,
    string MetadataName
);

internal sealed class CRFlagDefinitions(IEnumerable<CRFlag> Flags, int CRIndex)
{
    public IEnumerable<CRFlag> Flags { get; } = Flags;
    public int CRIndex { get; } = CRIndex;
}

internal sealed record RegisterBinding(string TargetName, string TargetMetadataName, int Bitness);

internal sealed class BoundRegister(IEnumerable<RegisterBinding> bindings, string name, string metadataName) : IBoundRegisterOrDefinition
{
    public string Name { get; } = name;
    public string MetadataName { get; } = metadataName;
    public IEnumerable<RegisterBinding> Bindings { get; } = bindings;
}

internal sealed class RegisterDefinition(string name, string metadataName, int bitness) : IBoundRegisterOrDefinition
{
    public string Name { get; } = name;
    public string MetadataName { get; } = metadataName;
    public int Bitness { get; } = bitness;
}

internal sealed class BaseVectorRegister(string name, string metadataName, int bitness, bool alsoCreateYmm, bool alsoCreateXmm)
{
    public string Name { get; } = name;
    public string MetadataName { get; } = metadataName;
    public int Bitness { get; } = bitness;
    public bool AlsoCreateYmm { get; } = alsoCreateYmm;
    public bool AlsoCreateXmm { get; } = alsoCreateXmm;
}

internal sealed class DocumentSkeleton(string @namespace, string typeName, bool allowRegisterInterface, IEnumerable<RegisterGroupNode> children)
{
    public string Namespace { get; } = @namespace;
    public string TypeName { get; } = typeName;
    public bool AllowRegisterInterface { get; } = allowRegisterInterface;
    public IEnumerable<RegisterGroupNode> Children { get; } = children;
}
