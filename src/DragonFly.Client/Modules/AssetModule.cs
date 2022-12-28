// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Assets;
using DragonFly.Client.Pages.Assets.Metadata;
using DragonFly.Client.Pages.Assets.Preview;
using DragonFly.Razor.Extensions;

namespace DragonFly.Client;

/// <summary>
/// AssetModule
/// </summary>
public class AssetModule : ClientModule
{
    public override string Name => "Asset";

    public override string Description => "Manage assets for content items";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.MainMenu().Add("Assets", "fa-regular fa-image", "asset");

        api.RegisterMetadata<ImageMetadata, ImageMetadataView>();
        api.RegisterMetadata<PdfMetadata, PdfMetadataView>();

        api.AssetPreview().Register<ImagePreviewView>(MimeTypes.WebP, MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp);
        api.AssetPreview().Register<PdfPreviewView>(MimeTypes.Pdf);
        api.AssetPreview().Register<SvgPreviewView>(MimeTypes.Svg);
    }
}
