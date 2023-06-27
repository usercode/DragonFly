// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ReferenceFieldQuery
/// </summary>
public class ReferenceFieldQuery : FieldQuery
{
    /// <summary>
    /// ContentItemId
    /// </summary>
    public Guid? ContentItemId { get; set; }

    public override bool IsEmpty() => ContentItemId == null;
}
