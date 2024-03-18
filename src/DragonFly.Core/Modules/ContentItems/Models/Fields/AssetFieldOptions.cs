// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetFieldOptions
/// </summary>
public class AssetFieldOptions : FieldOptions
{
    public AssetFieldOptions()
    {
    }

    public bool ShowPreview { get; set; }

    public override ContentField CreateContentField()
    {
        return new AssetField();
    }
}
