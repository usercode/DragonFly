// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceQuery
/// </summary>
public class ReferenceQuery : FieldQuery
{
    /// <summary>
    /// ContentItemId
    /// </summary>
    public Guid? ContentItemId { get; set; }

    public override bool IsEmpty() => ContentItemId == null;
}
