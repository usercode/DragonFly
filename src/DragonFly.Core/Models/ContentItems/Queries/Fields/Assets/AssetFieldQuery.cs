// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

/// <summary>
/// AssetFieldQuery
/// </summary>
public class AssetFieldQuery : FieldQuery
{
    /// <summary>
    /// AssetId
    /// </summary>
    public Guid? AssetId { get; set; }

    public override bool IsEmpty() => AssetId == null;
}
