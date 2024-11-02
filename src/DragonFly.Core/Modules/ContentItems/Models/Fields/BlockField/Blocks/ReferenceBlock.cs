// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceBlock
/// </summary>
public class ReferenceBlock : Block, IReferencedContent
{
    public override string CssIcon => "fa-solid fa-list";

    /// <summary>
    /// ContentId
    /// </summary>
    public Guid? ContentId { get; set; }

    /// <summary>
    /// ContentSchema
    /// </summary>
    public string? ContentSchema { get; set; }

    public ContentReference[] GetReferences()
    {
        if (ContentId != null && ContentSchema != null)
        {
            return [new ContentReference(ContentSchema, ContentId.Value)];
        }

        return [];
    }
}
