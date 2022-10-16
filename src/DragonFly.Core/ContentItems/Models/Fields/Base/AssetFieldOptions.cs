// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// AssetFieldOptions
/// </summary>
public class AssetFieldOptions : ContentFieldOptions
{
    public AssetFieldOptions()
    {
    }

    public bool ShowPreview { get; set; }

    public override IContentField CreateContentField()
    {
        return new AssetField();
    }
}
