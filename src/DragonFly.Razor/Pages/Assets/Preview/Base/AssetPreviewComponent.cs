// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Components;

namespace DragonFly.Razor.Assets;

public abstract class AssetPreviewComponent : ComponentBase, IAssetPreviewComponent
{
    [Parameter]
    public Asset Asset { get; set; }
}
