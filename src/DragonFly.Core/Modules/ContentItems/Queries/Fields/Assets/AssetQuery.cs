// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetQuery
/// </summary>
public class AssetQuery : FieldQuery
{
    /// <summary>
    /// AssetId
    /// </summary>
    public Guid? AssetId { get; set; }

    public override bool IsEmpty() => AssetId == null;
}
