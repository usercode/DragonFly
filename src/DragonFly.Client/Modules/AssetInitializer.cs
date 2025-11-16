// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.Assets.Metadata;
using DragonFly.Client.Pages.Assets.Preview;
using DragonFly.Init;

namespace DragonFly.Client;

/// <summary>
/// AssetModule
/// </summary>
public class AssetInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {
        api.MainMenu.Add("Assets", "fa-regular fa-image", "asset");

        api.Metadatas.Add<ImageMetadata>().WithMetadataView<ImageMetadataView>();
        api.Metadatas.Add<PdfMetadata>().WithMetadataView<PdfMetadataView>();
        api.Metadatas.Add<VideoMetadata>().WithMetadataView<VideoMetadataView>();

        api.AssetPreviews.Add<ImagePreviewView>(MimeTypes.WebP, MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp);
        api.AssetPreviews.Add<PdfPreviewView>(MimeTypes.Pdf);
        api.AssetPreviews.Add<SvgPreviewView>(MimeTypes.Svg);
        api.AssetPreviews.Add<VideoPreviewView>(MimeTypes.Mp4, MimeTypes.Ogg, MimeTypes.WebM);

        return Task.CompletedTask;
    }
}
