// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
