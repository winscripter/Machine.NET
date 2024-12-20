using System.Xml.Linq;

namespace RegistersXmlToCSharp;

internal static class Reader
{
    private static RegisterBinding ReadRegisterBinding(XElement element)
    {
        if (element.Name != "Binding")
            throw new ArgumentException("Element name must be 'Binding'", nameof(element));

        string targetName = element.Attribute("TargetName")?.Value ?? throw new InvalidOperationException("'Binding' element must have attribute named 'TargetName'");
        string targetMetadataName = element.Attribute("TargetMetadataName")?.Value ?? throw new InvalidOperationException("'Binding' element must have attribute named 'TargetMetadataName'");
        int bitness = int.Parse(element.Attribute("TargetBitness")?.Value ?? throw new InvalidOperationException("'Binding' element must have attribute named 'Bitness'"));

        return new RegisterBinding(targetName, targetMetadataName, bitness);
    }

    private static BoundRegister ReadBoundRegister(XElement element)
    {
        if (element.Name != "BoundRegister")
            throw new ArgumentException("Element name must be 'BoundRegister'", nameof(element));

        string name = element.Attribute("Name")?.Value ?? throw new InvalidOperationException("'BoundRegister' element must have attribute named 'Name'");
        string metadataName = element.Attribute("MetadataName")?.Value ?? throw new InvalidOperationException("'BoundRegister' element must have attribute named 'MetadataName'");
        IEnumerable<RegisterBinding> bindings = element.Elements().Select(ReadRegisterBinding);

        return new BoundRegister(bindings, name, metadataName);
    }

    private static RegisterDefinition ReadRegisterDefinition(XElement element)
    {
        if (element.Name != "RegisterDefinition")
            throw new ArgumentException("Element name must be 'RegisterDefinition'", nameof(element));

        string name = element.Attribute("Name")?.Value ?? throw new InvalidOperationException("'Binding' element must have attribute named 'Name'");
        string metadataName = element.Attribute("MetadataName")?.Value ?? throw new InvalidOperationException("'Binding' element must have attribute named 'MetadataName'");
        int bitness = int.Parse(element.Attribute("Bitness")?.Value ?? throw new InvalidOperationException("'Binding' element must have attribute named 'Bitness'"));

        return new RegisterDefinition(name, metadataName, bitness);
    }

    private static BaseVectorRegister ReadBaseVectorRegister(XElement element)
    {
        if (element.Name != "BaseVectorRegister")
            throw new ArgumentException("Element name must be 'BaseVectorRegister'", nameof(element));

        string name = element.Attribute("Name")?.Value ?? throw new InvalidOperationException("'BaseVectorRegister' element must have attribute named 'Name'");
        string metadataName = element.Attribute("MetadataName")?.Value ?? throw new InvalidOperationException("'BaseVectorRegister' element must have attribute named 'MetadataName'");
        int bitness = int.Parse(element.Attribute("Bitness")?.Value ?? throw new InvalidOperationException("'BaseVectorRegister' element must have attribute named 'Bitness'"));
        bool alsoCreateYmm = bool.Parse(element.Attribute("AlsoCreateYmm")?.Value ?? throw new InvalidOperationException("'BaseVectorRegister' element must have attribute named 'AlsoCreateYmm'"));
        bool alsoCreateXmm = bool.Parse(element.Attribute("AlsoCreateXmm")?.Value ?? throw new InvalidOperationException("'BaseVectorRegister' element must have attribute named 'AlsoCreateXmm'"));

        return new(name, metadataName, bitness, alsoCreateYmm, alsoCreateXmm);
    }

    private static RegisterGroupNode ReadRegisterGroup(XElement element)
    {
        if (!(element.Name.LocalName is "GeneralPurpose"
                or "Segment"
                or "VectorRegisters"
                or "K"
                or "Other"
                or "CRFlags"
                or "CpuFlags"
                or "GeneralPurposePart"))
            throw new ArgumentException("This is not a valid register group based on element name", nameof(element));

        switch (element.Name.LocalName)
        {
            case "GeneralPurpose":
                {
                    IEnumerable<IBoundRegisterOrDefinition> children =
                        element.Elements().Select(e => e.Name == "BoundRegister" ? (IBoundRegisterOrDefinition)ReadBoundRegister(e) : ReadRegisterDefinition(e));

                    return new GeneralPurposeGroupNode(children);
                }

            case "Segment":
                {
                    IEnumerable<RegisterDefinition> children = element.Elements().Select(ReadRegisterDefinition);
                    return new SegmentGroupNode(children);
                }

            case "VectorRegisters":
                {
                    IEnumerable<BaseVectorRegister> children = element.Elements().Select(ReadBaseVectorRegister);
                    return new VectorRegistersGroupNode(children);
                }

            case "K":
                {
                    IEnumerable<RegisterDefinition> children = element.Elements().Select(ReadRegisterDefinition);
                    return new KGroupNode(children);
                }

            case "Other":
                {
                    IEnumerable<RegisterDefinition> children = element.Elements().Select(ReadRegisterDefinition);
                    return new OtherGroupNode(children);
                }

            case "CRFlags":
                {
                    IEnumerable<CRFlagDefinitions> defs = element.Elements().Select(ReadCRFlagDefinitions);
                    return new CRFlagsGroupNode(defs);
                }

            case "CpuFlags":
                {
                    IEnumerable<BoundFlag> boundFlags = element.Elements().Select(ReadBoundFlag);
                    return new CpuFlagsGroupNode(boundFlags);
                }

            case "GeneralPurposePart":
                {
                    IEnumerable<GeneralPurposeRegisterPart> part = element.Elements().Select(ReadGprPart);
                    return new GeneralPurposePartGroupNode(part);
                }

            default:
                throw new InvalidOperationException();
        }
    }

    private static DocumentSkeleton ReadRoot(XElement element)
    {
        if (element.Name.LocalName != "Registers")
            throw new ArgumentException("Invalid root element: name must be 'Registers'", nameof(element));

        string @namespace = element.Attribute("Namespace")?.Value ?? throw new InvalidOperationException("'Registers' element must have attribute named 'Namespace'");
        string typeName = element.Attribute("TypeName")?.Value ?? throw new InvalidOperationException("'Registers' element must have attribute named 'TypeName'");
        bool allowRegisterInterface = bool.Parse(element.Attribute("AllowRegisterInterface")?.Value ?? throw new InvalidOperationException("'Registers' element must have attribute named 'AllowRegisterInterface'"));
        IEnumerable<RegisterGroupNode> children = element.Elements().Select(ReadRegisterGroup);

        return new DocumentSkeleton(@namespace, typeName, allowRegisterInterface, children);
    }

    private static BoundFlag ReadBoundFlag(XElement element)
    {
        if (element.Name != "BoundFlag")
            throw new ArgumentException("Element name must be 'BoundFlag'", nameof(element));

        int bit = int.Parse(element.Attribute("Bit")?.Value ?? throw new InvalidOperationException("'BoundFlag' element must have attribute named 'Bit'"));
        string name = element.Attribute("Name")?.Value ?? throw new InvalidOperationException($"'BoundFlag' element must have attribute named 'Name'");
        string aka = element.Attribute("AlsoKnownAs")?.Value ?? throw new InvalidOperationException($"'BoundFlag' element must have attribute named 'AlsoKnownAs'");

        return new BoundFlag(bit, name, aka);
    }

    private static CRFlag ReadCRFlag(XElement element)
    {
        if (element.Name != "CRFlag")
            throw new ArgumentException("Element name must be 'CRFlag'", nameof(element));

        int bit = int.Parse(element.Attribute("Bit")?.Value ?? throw new InvalidOperationException("'CRFlag' element must have attribute named 'Bit'"));
        string name = element.Attribute("Name")?.Value ?? throw new InvalidOperationException($"'CRFlag' element must have attribute named 'Name'");
        string aka = element.Attribute("AlsoKnownAs")?.Value ?? throw new InvalidOperationException($"'CRFlag' element must have attribute named 'AlsoKnownAs'");

        return new CRFlag(bit, name, aka);
    }

    private static CRFlagDefinitions ReadCRFlagDefinitions(XElement element)
    {
        if (element.Name != "CRFlagDefinitions")
            throw new ArgumentException("Element name must be 'CRFlagDefinitions'", nameof(element));

        int crIndex = int.Parse(element.Attribute("CRIndex")?.Value ?? throw new InvalidOperationException("'CRFlagDefinitions' element must have attribute named 'CRIndex'"));
        return new CRFlagDefinitions(element.Elements().Select(ReadCRFlag), crIndex);
    }

    private static GeneralPurposeRegisterPart ReadGprPart(XElement element)
    {
        if (element.Name != "GeneralPurposeRegisterPart")
            throw new ArgumentException("Element name must be 'GeneralPurposeRegisterPart'", nameof(element));

        string targetName = element.Attribute("TargetName")?.Value ?? throw new InvalidOperationException($"'GeneralPurposeRegisterPart' element must have attribute named 'TargetName'");
        string targetMetadataName = element.Attribute("TargetMetadataName")?.Value ?? throw new InvalidOperationException($"'GeneralPurposeRegisterPart' element must have attribute named 'TargetMetadataName'");
        int bitSplit = int.Parse(element.Attribute("BitSplit")?.Value ?? throw new InvalidOperationException("'GeneralPurposeRegisterPart' element must have attribute named 'BitSplit'"));
        string direction = element.Attribute("Direction")?.Value ?? throw new InvalidOperationException($"'GeneralPurposeRegisterPart' element must have attribute named 'Direction'");
        string name = element.Attribute("Name")?.Value ?? throw new InvalidOperationException($"'GeneralPurposeRegisterPart' element must have attribute named 'Name'");
        string metadataName = element.Attribute("MetadataName")?.Value ?? throw new InvalidOperationException($"'GeneralPurposeRegisterPart' element must have attribute named 'MetadataName'");

        return new GeneralPurposeRegisterPart(targetName, targetMetadataName, bitSplit, direction, name, metadataName);

    }

    public static DocumentSkeleton Load(string text)
    {
        XDocument document = XDocument.Parse(text);
        return ReadRoot(document.Root ?? throw new InvalidOperationException("Missing root element"));
    }
}
