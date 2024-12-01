// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Threading.Tasks;
using DragonFly.Client.Pages.Assets.Metadata;
using DragonFly.Client.Pages.Assets.Preview;
using DragonFly.Init;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DragonFly.Client;

/// <summary>
/// AssetModule
/// </summary>
public class AssetInitializer : IInitialize
{
    public Task ExecuteAsync(IDragonFlyApi api)
    {
        api.MainMenu().Add("Assets", new Icons.Filled.Size24.Image(), "asset");

        api.Metadata().Add<ImageMetadata>().WithMetadataView<ImageMetadataView>();
        api.Metadata().Add<PdfMetadata>().WithMetadataView<PdfMetadataView>();
        api.Metadata().Add<VideoMetadata>().WithMetadataView<VideoMetadataView>();

        api.AssetPreview().Add<ImagePreviewView>(MimeTypes.WebP, MimeTypes.Jpeg, MimeTypes.Png, MimeTypes.Gif, MimeTypes.Bmp);
        api.AssetPreview().Add<PdfPreviewView>(MimeTypes.Pdf);
        api.AssetPreview().Add<SvgPreviewView>(MimeTypes.Svg);
        api.AssetPreview().Add<VideoPreviewView>(MimeTypes.Mp4, MimeTypes.Ogg, MimeTypes.WebM);

        return Task.CompletedTask;
    }
}
