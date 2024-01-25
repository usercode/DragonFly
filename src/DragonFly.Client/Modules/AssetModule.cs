// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.Assets.Metadata;
using DragonFly.Client.Pages.Assets.Preview;

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

        api.AssetMetadata().Add<ImageMetadata>().WithMetadataView<ImageMetadataView>();
        api.AssetMetadata().Add<PdfMetadata>().WithMetadataView<PdfMetadataView>();
        api.AssetMetadata().Add<VideoMetadata>().WithMetadataView<VideoMetadataView>();

        api.AssetPreview().Add<ImagePreviewView>(MimeTypes.WebP, MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp);
        api.AssetPreview().Add<PdfPreviewView>(MimeTypes.Pdf);
        api.AssetPreview().Add<SvgPreviewView>(MimeTypes.Svg);
        api.AssetPreview().Add<VideoPreviewView>(MimeTypes.Mp4, MimeTypes.Ogg, MimeTypes.WebM);
    }
}
