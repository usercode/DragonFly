// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.BlockField;

/// <summary>
/// ReferenceBlock
/// </summary>
public class ReferenceBlock : Block
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
}
