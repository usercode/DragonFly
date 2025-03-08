// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// BlockField
/// </summary>
[Field]
[FieldOptions(typeof(BlockFieldOptions))]
public partial class BlockField : SingleValueField<string>, IReferencedContent
{
    public BlockField()
    {
    }

    public ContentReference[] GetReferences()
    {
        Document document = BlockFieldSerializer.DeserializeAsync(Value).GetAwaiter().GetResult();

        return document.EnumerateBlocks()
                                .Select(x => x.Block)
                                .OfType<IReferencedContent>()
                                .SelectMany(x => x.GetReferences())
                                .ToArray();
    }
}
